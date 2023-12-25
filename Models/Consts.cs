using Google.Apis.Drive.v3;

namespace Sharable.Models
{
    public class GoogleDriveArgs
    {
        public string AppName { get; set; } = "";
        public string APIKey { get; set; } = "";
        public string GoogleSpreadSheetId { get; set; } = "";
        public string GoogleMimeType { get; set; } = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        public string[] GoogleDriveScopes { get; set; } = [DriveService.Scope.DriveReadonly];
    }

    public class OneDriveArgs
    {
        public string OneDriveAppId { get; set; } = "";
        public string OneDriveTenantId { get; set; } = "consumers";
        public string OneDriveClientSecret { get; set; } = "";
        public string OneDriveFolderId { get; set; } = "";
        public string OneDriveRefreshToken { get; set; } = "";
        public string[] OneDriveScopes { get; set; } = ["Files.ReadWrite"];
    }
}