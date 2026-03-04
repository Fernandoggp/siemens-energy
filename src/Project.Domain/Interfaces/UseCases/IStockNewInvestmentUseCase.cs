using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface IStockNewInvestmentUseCase
    {
        Task<dynamic> ExecuteAsync(List<StockNewInvestmentEntity> stocks, decimal value);
    }
}
