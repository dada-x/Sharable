using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharable.Models;

namespace Sharable.Services
{
    public class SourceSettingService : ISourceSettingService
    {
        public GoogleDriveArgs GoogleDrive { get; set; }
        public OneDriveArgs OneDrive { get; set; } = new();

        public SourceSettingService()
        {
            var reader = new StreamReader("Secret/SourceSetting.json");
            var json = reader.ReadToEnd();
            var setting = JsonConvert.DeserializeObject<JObject>(json) ?? [];
            GoogleDrive = setting[nameof(GoogleDrive)]?.ToObject<GoogleDriveArgs>() ?? new();

            reader.Dispose();
        }
    }
}