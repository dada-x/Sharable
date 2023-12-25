using System.Text.Json.Nodes;
using Google.Apis.Auth.OAuth2;
using Microsoft.Kiota.Abstractions.Authentication;

namespace Sharable.Models
{
    public interface IAuthService
    {
        GoogleCredential GetGoogleDriveCredential();
        IAuthenticationProvider GetOneDriveCredential();
    }

    public interface ISourceSettingService
    {
        GoogleDriveArgs GoogleDrive { get; set; }
        OneDriveArgs OneDrive { get; set; }
    }

    public interface IDataTableProcessor
    {
        XGPGame[] ParseXGPMasterListFromGoogleDrive();
    }

    public interface IExcelService
    {
        Task<XGPGame[]> ParseXGPStream(Stream stream);
    }

    public interface IGameInfoService
    {
        Task<List<XGPGame>> Parse(IEnumerable<XGPGame> games);
    }
}