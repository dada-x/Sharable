using System.Text.Json;
using System.Text.Json.Nodes;
using Google.Apis.Auth.OAuth2;
using Microsoft.Kiota.Abstractions.Authentication;
using Newtonsoft.Json;
using Sharable.Models;
using Sharable.Services.OneDrive;

namespace Sharable.Services
{
    public class AuthService(ISourceSettingService sourceSettingService) : IAuthService
    {
        private readonly ISourceSettingService _setting = sourceSettingService;

        public GoogleCredential GetGoogleDriveCredential()
        {
            var stream = new FileStream("Secret/Google.json", FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream).CreateScoped(_setting.GoogleDrive.GoogleDriveScopes);

            stream.Dispose();

            return credential;
        }

        public IAuthenticationProvider GetOneDriveCredential()
        {
            throw new NotImplementedException();
        }
    }
}