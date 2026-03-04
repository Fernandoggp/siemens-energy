using Newtonsoft.Json;

namespace Project.Application.Dtos
{
    public class CompanyRatiosDto
    {
        [JsonProperty("OPERATING_INCOME_QUALITY_COEFFICIENT")]
        public RatioPeriod OperatingIncomeQualityCoefficient { get; set; }

        [JsonProperty("CAGR_4Y_REVENUE")]
        public RatioPeriod Cagr4YRevenue { get; set; }

        [JsonProperty("EBIT_MARGIN")]
        public RatioPeriod EbitMargin { get; set; }

        [JsonProperty("CAGR_4Y_EBITDA")]
        public RatioPeriod Cagr4YEbitda { get; set; }

        [JsonProperty("EBITDA_MARGIN")]
        public RatioPeriod EbitdaMargin { get; set; }

        [JsonProperty("CAGR_2Y_NET_INCOME")]
        public RatioPeriod Cagr2YNetIncome { get; set; }

        [JsonProperty("CAGR_1Y_REVENUE")]
        public RatioPeriod Cagr1YRevenue { get; set; }

        [JsonProperty("NET_MARGIN")]
        public RatioPeriod NetMargin { get; set; }

        [JsonProperty("INTEREST_COVERAGE_RATIO")]
        public RatioPeriod InterestCoverageRatio { get; set; }

        [JsonProperty("DL_TO_PL")]
        public RatioPeriod DlToPl { get; set; }

        [JsonProperty("PAYOUT")]
        public RatioPeriod Payout { get; set; }

        [JsonProperty("CAGR_2Y_REVENUE")]
        public RatioPeriod Cagr2YRevenue { get; set; }

        [JsonProperty("DEBT_TO_DEBT_AND_EQUITY")]
        public RatioPeriod DebtToDebtAndEquity { get; set; }

        [JsonProperty("QUICK_RATIO")]
        public RatioPeriod QuickRatio { get; set; }

        [JsonProperty("ASSETS_TO_EQUITY")]
        public RatioPeriod AssetsToEquity { get; set; }

        [JsonProperty("SHAREHOLDERS_EQUITY_TO_ASSETS")]
        public RatioPeriod ShareholdersEquityToAssets { get; set; }

        [JsonProperty("DL_TO_EBITDA")]
        public RatioPeriod DlToEbitda { get; set; }

        [JsonProperty("CAGR_5Y_EBITDA")]
        public RatioPeriod Cagr5YEbitda { get; set; }

        [JsonProperty("CONTINUING_INCOME_MARGIN")]
        public RatioPeriod ContinuingIncomeMargin { get; set; }

        [JsonProperty("GROSS_MARGIN")]
        public RatioPeriod GrossMargin { get; set; }

        [JsonProperty("CAGR_3Y_EBITDA")]
        public RatioPeriod Cagr3YEbitda { get; set; }

        [JsonProperty("LIABILITIES_TO_ASSETS")]
        public RatioPeriod LiabilitiesToAssets { get; set; }

        [JsonProperty("IMMEDIATE_LIQUIDITY")]
        public RatioPeriod ImmediateLiquidity { get; set; }

        [JsonProperty("CAGR_2Y_EBITDA")]
        public RatioPeriod Cagr2YEbitda { get; set; }

        [JsonProperty("NET_OPERATING_PROFIT_MARGIN")]
        public RatioPeriod NetOperatingProfitMargin { get; set; }

        [JsonProperty("ROIC")]
        public RatioPeriod Roic { get; set; }

        [JsonProperty("CLEAN_EBITDA_MARGIN")]
        public RatioPeriod CleanEbitdaMargin { get; set; }

        [JsonProperty("CONTINUING_TO_NET_INCOME")]
        public RatioPeriod ContinuingToNetIncome { get; set; }

        [JsonProperty("FINANCIAL_LEVERAGE")]
        public RatioPeriod FinancialLeverage { get; set; }

        [JsonProperty("ROE")]
        public RatioPeriod Roe { get; set; }

        [JsonProperty("ROCE")]
        public RatioPeriod Roce { get; set; }

        [JsonProperty("INTEREST_COVERAGE_WITH_CASH")]
        public RatioPeriod InterestCoverageWithCash { get; set; }

        [JsonProperty("CAGR_4Y_NET_INCOME")]
        public RatioPeriod Cagr4YNetIncome { get; set; }

        [JsonProperty("CURRENT_RATIO")]
        public RatioPeriod CurrentRatio { get; set; }

        [JsonProperty("SALES_TO_CAPITAL")]
        public RatioPeriod SalesToCapital { get; set; }

        [JsonProperty("ROA")]
        public RatioPeriod Roa { get; set; }

        [JsonProperty("CAGR_5Y_NET_INCOME")]
        public RatioPeriod Cagr5YNetIncome { get; set; }

        [JsonProperty("CAGR_3Y_NET_INCOME")]
        public RatioPeriod Cagr3YNetIncome { get; set; }

        [JsonProperty("CAGR_5Y_REVENUE")]
        public RatioPeriod Cagr5YRevenue { get; set; }

        [JsonProperty("GENERAL_LIQUIDITY")]
        public RatioPeriod GeneralLiquidity { get; set; }

        [JsonProperty("DL_TO_EBIT")]
        public RatioPeriod DlToEbit { get; set; }

        [JsonProperty("CAGR_3Y_REVENUE")]
        public RatioPeriod Cagr3YRevenue { get; set; }

        [JsonProperty("CAGR_1Y_EBITDA")]
        public RatioPeriod Cagr1YEbitda { get; set; }

        [JsonProperty("CAPEX_TO_D_AND_A")]
        public RatioPeriod CapexToDA { get; set; }

        [JsonProperty("CAGR_1Y_NET_INCOME")]
        public RatioPeriod Cagr1YNetIncome { get; set; }

        [JsonProperty("DEBT_RATIO")]
        public RatioPeriod DebtRatio { get; set; }

        [JsonProperty("ASSET_TURNOVER")]
        public RatioPeriod AssetTurnover { get; set; }

        [JsonProperty("INCOME_QUALITY_COEFFICIENT")]
        public RatioPeriod IncomeQualityCoefficient { get; set; }
    }

    public class RatioPeriod
    {
        [JsonProperty("ANNUAL")]
        public List<RatioPeriodData> Annual { get; set; }

        [JsonProperty("QUARTERLY")]
        public List<RatioPeriodData> Quarterly { get; set; }

        [JsonProperty("TTM")]
        public List<RatioPeriodData> Ttm { get; set; }

        [JsonProperty("YTD")]
        public List<RatioPeriodData> Ytd { get; set; }
    }

    public class RatioPeriodData
    {
        [JsonProperty("value")]
        public double? Value { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("reference_date")]
        public DateTime ReferenceDate { get; set; }

        [JsonProperty("publish_date")]
        public DateTime PublishDate { get; set; }

        [JsonProperty("retrieval_date")]
        public DateTime RetrievalDate { get; set; }
    }
}
