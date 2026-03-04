using Newtonsoft.Json;

namespace Project.Application.Dtos
{
    public class CompanyDto
    {
        [JsonProperty("company_id")]
        public string CompanyId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("trading_name")]
        public string TradingName { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("common_shares")]
        public string CommonShares { get; set; }

        [JsonProperty("preferred_shares")]
        public string PreferredShares { get; set; }

        [JsonProperty("total_shares")]
        public string TotalShares { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("characteristics")]
        public string Characteristics { get; set; }
    }

}
