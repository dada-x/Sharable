namespace Sharable.Models
{
    public class XGPGame
    {
        public required string CnName { get; set; }
        public required string EnName { get; set; }
        public string SteamAppId { get; set; } = string.Empty;
        public string SteamPrice { get; set; } = string.Empty;
        public string SteamDeveloper { get; set; } = string.Empty;
        public required string Genre { get; set; }
        public int? Metacritic { get; set; }
        public required string Platform { get; set; }
        public required string Status { get; set; }
        public required DateTime AddTS { get; set; }
        public required DateTime ReleaseTS { get; set; }
    }
}