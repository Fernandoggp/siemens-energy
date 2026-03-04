using Newtonsoft.Json;

namespace Project.Application.Dtos
{
    public class CompanyRatiosValuationDto
    {
        [JsonProperty("PRICE_TO_BOOK_CS")]
        public RatioPeriod PriceToBookCs { get; set; }

        [JsonProperty("PRICE_TO_ASSET_CS")]
        public RatioPeriod PriceToAssetCs { get; set; }

        [JsonProperty("PRICE_TO_EBT_PS")]
        public RatioPeriod PriceToEbtPs { get; set; }

        [JsonProperty("ENTERPRISE_VALUE")]
        public RatioPeriod EnterpriseValue { get; set; }

        [JsonProperty("PRICE_TO_WORKING_CAPITAL_PS")]
        public RatioPeriod PriceToWorkingCapitalPs { get; set; }

        [JsonProperty("EV_TO_EBITDA")]
        public RatioPeriod EvToEbitda { get; set; }

        [JsonProperty("PRICE_TO_EBITDA_PS")]
        public RatioPeriod PriceToEbitdaPs { get; set; }

        [JsonProperty("GRAHAM_NUMBER_UPSIDE_PS")]
        public RatioPeriod GrahamNumberUpsidePs { get; set; }

        [JsonProperty("EV_TO_INVESTED_CAPITAL")]
        public RatioPeriod EvToInvestedCapital { get; set; }

        [JsonProperty("PEG_RATIO_CS")]
        public RatioPeriod PegRatioCs { get; set; }

        [JsonProperty("PRICE_TO_EBIT_PS")]
        public RatioPeriod PriceToEbitPs { get; set; }

        [JsonProperty("PRICE_TO_EBT_CS")]
        public RatioPeriod PriceToEbtCs { get; set; }

        [JsonProperty("PEG_RATIO_PS")]
        public RatioPeriod PegRatioPs { get; set; }

        [JsonProperty("PRICE_TO_SALES_PS")]
        public RatioPeriod PriceToSalesPs { get; set; }

        [JsonProperty("PRICE_TO_ASSET_PS")]
        public RatioPeriod PriceToAssetPs { get; set; }

        [JsonProperty("PRICE_TO_EBITDA_CS")]
        public RatioPeriod PriceToEbitdaCs { get; set; }

        [JsonProperty("PRICE_TO_EARNINGS_CS")]
        public RatioPeriod PriceToEarningsCs { get; set; }

        [JsonProperty("PRICE_TO_OCF_PS")]
        public RatioPeriod PriceToOcfPs { get; set; }

        [JsonProperty("PRICE_TO_BOOK_PS")]
        public RatioPeriod PriceToBookPs { get; set; }

        [JsonProperty("MARKET_CAP")]
        public RatioPeriod MarketCap { get; set; }

        [JsonProperty("EV_TO_EBIT")]
        public RatioPeriod EvToEbit { get; set; }

        [JsonProperty("PRICE_TO_WORKING_CAPITAL_CS")]
        public RatioPeriod PriceToWorkingCapitalCs { get; set; }

        [JsonProperty("GRAHAM_NUMBER")]
        public RatioPeriod GrahamNumber { get; set; }

        [JsonProperty("GRAHAM_NUMBER_UPSIDE_CS")]
        public RatioPeriod GrahamNumberUpsideCs { get; set; }

        [JsonProperty("PRICE_TO_EARNINGS_PS")]
        public RatioPeriod PriceToEarningsPs { get; set; }

        [JsonProperty("PRICE_TO_SALES_CS")]
        public RatioPeriod PriceToSalesCs { get; set; }

        [JsonProperty("PRICE_TO_OCF_CS")]
        public RatioPeriod PriceToOcfCs { get; set; }

        [JsonProperty("PRICE_TO_EBIT_CS")]
        public RatioPeriod PriceToEbitCs { get; set; }

        [JsonProperty("EV_TO_REVENUE")]
        public RatioPeriod EvToRevenue { get; set; }
    }
}
