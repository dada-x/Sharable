using Google.Apis.Drive.v3;
using Google.Apis.Services;
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

        public async Task<XGPGame[]> ParseXGPMasterListFromGoogleDrive()
        {
            try
            {
                var request = _googleDriveService.Files.Export(_setting.GoogleDrive.GoogleSpreadSheetId, _setting.GoogleDrive.GoogleMimeType);
                var mStream = new MemoryStream();

                var res = request.ExecuteAsStream();

                return await _excelService.ParseXGPStream(res);
            }
            catch
            {
                throw;
            }
        }
    }
}