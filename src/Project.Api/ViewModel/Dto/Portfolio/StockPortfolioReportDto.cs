using Project.Domain.Entities;

namespace Project.Api.ViewModel.Dto.Portfolio
{
    public class StockPortfolioReportDto
    {
        public List<SimpleStock> Stocks { get; set; }
        public int StocksNumber { get; set; }
    }
    public class SimpleStock
    {
        public string Code { get; set; }
        public decimal Value { get; set; }
    }
}
