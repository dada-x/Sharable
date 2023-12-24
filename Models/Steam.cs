using System.Text.Json;
using Newtonsoft.Json;

namespace Sharable.Models
{
    public class SteamApp
    {
        public required string AppId { get; set; }
        [JsonProperty("name")]
        public required string EnName { get; set; }
        public required string CnName { get; set; }
        public required string Price { get; set; }
    }
}