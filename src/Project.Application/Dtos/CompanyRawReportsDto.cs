using Newtonsoft.Json;

namespace Project.Application.Dtos
{
    public class CompanyRawReportsDto
    {
        [JsonProperty("BPA")]
        public Dictionary<string, BalanceSheetAssetData> BPA { get; set; }

        [JsonProperty("BPP")]
        public Dictionary<string, BalanceSheetLiabilityData> BPP { get; set; }
    }

    public class BalanceSheetAssetData
    {
        [JsonProperty("Ativo Total")]
        public decimal? AtivoTotal { get; set; }
    }

    public class BalanceSheetLiabilityData
    {
        [JsonProperty("Passivo Total")]
        public decimal? PassivoTotal { get; set; }
    }
}
