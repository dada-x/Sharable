using System.Text.Json;
using Microsoft.Kiota.Abstractions;
using Microsoft.Kiota.Abstractions.Authentication;
using Sharable.Models;

namespace Sharable.Services.OneDrive
{
    public class OneDriveAuthProvider : IAuthenticationProvider
    {
        private readonly ISourceSettingService _setting;

        private readonly HttpClient _httpClient = new();

        public OneDriveAuthProvider(ISourceSettingService sourceSettingService)
        {
            _setting = sourceSettingService;
        }

        public async Task AuthenticateRequestAsync(RequestInformation request, Dictionary<string, object>? additionalAuthenticationContext = null, CancellationToken cancellationToken = default)
        {
            request.Headers.Add("Authorization", $"bearer {await GetToken()}");
        }

        private async Task<string> GetToken()
        {
            var content = new Dictionary<string, string>
        {
            { "scope", "Files.ReadWrite offline_access" },
            { "client_id", _setting.OneDrive.OneDriveAppId },
            { "grant_type", "refresh_token" },
            { "refresh_token", _setting.OneDrive.OneDriveRefreshToken },
            { "client_secret", _setting.OneDrive.OneDriveClientSecret }
        };

            var res = await _httpClient.PostAsync($"https://login.microsoftonline.com/{_setting.OneDrive.OneDriveTenantId}/oauth2/v2.0/token", new FormUrlEncodedContent(content));
            var resContent = await res.Content.ReadAsStringAsync();
            return JsonDocument.Parse(resContent)?.RootElement.GetProperty("access_token").GetString() ?? "";
        }
    }
}