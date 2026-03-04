using Newtonsoft.Json;

namespace Project.Application.Dtos;
public class CompaniesDto
{
    [JsonProperty("company_id")]
    public string CompanyId { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("trading_name")]
    public string TradingName { get; set; }

    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("tickers")]
    public string Tickers { get; set; }

    [JsonProperty("sector")]
    public string Sector { get; set; }
}
