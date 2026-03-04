using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface IStockService
    {
        Task<dynamic> CalculateNewInvestmentAsync(List<StockNewInvestmentEntity> stocks, decimal value);
    }
}
