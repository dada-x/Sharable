using System.Text.Json.Nodes;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Newtonsoft.Json;
using Sharable.Models;

namespace Sharable.Services
{
    public class DataTableProcessor : IDataTableProcessor
    {
        private readonly ISourceSettingService _setting;
        private readonly DriveService _googleDriveService;
        private readonly IExcelService _excelService;

        public DataTableProcessor(ISourceSettingService sourceSettingService, IExcelService excelService)
        {
            _setting = sourceSettingService;
            _excelService = excelService;
            _googleDriveService = new DriveService(new BaseClientService.Initializer()
            {
                ApiKey = _setting.GoogleDrive.APIKey,
                ApplicationName = _setting.GoogleDrive.AppName
            });
        }

        private static readonly string _locker = "locker";
        public XGPGame[] ParseXGPMasterListFromGoogleDrive()
        {
            lock (_locker)
            {
                try
                {
                    var request = _googleDriveService.Files.Export(_setting.GoogleDrive.GoogleSpreadSheetId, _setting.GoogleDrive.GoogleMimeType);
                    var resStream = request.ExecuteAsStreamAsync().Result;

                    var fileName = $"cache_{resStream.Length}";
                    var currFolder = Directory.GetCurrentDirectory();

                    var currVersion = Directory.GetFiles(currFolder)?.FirstOrDefault(file => file.EndsWith(fileName));
                    if (currVersion != null)
                    {
                        var json = File.ReadAllText(currVersion);
                        return JsonConvert.DeserializeObject<XGPGame[]>(json) ?? [];
                    }

                    var result = _excelService.ParseXGPStream(resStream).Result;

                    File.WriteAllText(fileName, JsonConvert.SerializeObject(result));

                    resStream.Dispose();

                    return result;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}