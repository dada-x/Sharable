using FuzzySharp;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sharable.Models;

namespace Sharable.Services
{
    public class GameInfoService(IHttpClientFactory httpClientFactory) : IGameInfoService
    {

        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private List<SteamApp>? _steamAppIdList;

        private async Task<List<SteamApp>> GetSteamAppIdList()
        {
            if (_steamAppIdList == null)
            {
                await FetchSteamAppIdList();
            }

            return _steamAppIdList ?? [];
        }

        private async Task FetchSteamAppIdList()
        {
            var requestClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.steampowered.com/ISteamApps/GetAppList/v2/")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };

            var response = await requestClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var resString = await response.Content.ReadAsStringAsync();
                _steamAppIdList = [.. (JsonConvert.DeserializeObject<JToken>(resString)?["applist"]?["apps"]?.ToObject<SteamApp[]>() ?? [])];
            }
        }

        public async Task<List<XGPGame>> Parse(IEnumerable<XGPGame> games)
        {

            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                Headers =
                {
                    { HeaderNames.Accept, "application/json" }
                }
            };

            var ids = new List<string>();
            var steamAppIdList = await GetSteamAppIdList();
            games.Take(10).ToList().ForEach(game =>
            {
                game.SteamAppId = steamAppIdList?.FirstOrDefault(id => Fuzz.Ratio(id.EnName, game.EnName) > 90)?.AppId ?? string.Empty;
            });

            await BulkFetch(games.Take(10));

            return games.Take(10).ToList();
        }

        private const string TrimC = "\"";
        private async Task BulkFetch(IEnumerable<XGPGame> games)
        {


            foreach (var game in games.Where(game => game.SteamAppId != string.Empty))
            {
                var requestClient = _httpClientFactory.CreateClient();
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"https://store.steampowered.com/api/appdetails/?appids={game.SteamAppId}&l=schinese"),
                    Headers =
                    {
                        { HeaderNames.Accept, "application/json" }
                    }
                };

                var response = await requestClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var resString = await response.Content.ReadAsStringAsync();

                    game.CnName = JsonConvert.DeserializeObject<JToken>(resString)?[game.SteamAppId]?["data"]?["name"]?.ToString() ?? "";
                    game.SteamPrice = JsonConvert.DeserializeObject<JToken>(resString)?[game.SteamAppId]?["data"]?["price_overview"]?["final_formatted"]?.ToString() ?? "";
                    game.SteamDeveloper = JsonConvert.DeserializeObject<JToken>(resString)?[game.SteamAppId]?["data"]?["developers"]?.ToString() ?? "";

                    game.CnName = game.CnName.Replace(TrimC, string.Empty);
                }
            }
        }
    }
}