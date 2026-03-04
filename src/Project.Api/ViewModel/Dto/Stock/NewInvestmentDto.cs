using Project.Api.ViewModel.Dto.Portfolio;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Stock
{
    public class StockNewInvestmentDto
    {
        public List<StockNewInvestment> Stocks { get; set; }
        public decimal NewInvestmentValue { get; set; }
    }
    public class StockNewInvestment
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Value { get; set; }
        [Required]
        public int DesiredPercentage {  get; set; } 
    }
}
