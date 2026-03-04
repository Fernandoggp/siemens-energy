using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface IStockPortfolioReportUseCase
    {
        Task<dynamic> ExecuteAsync(List<SimpleStockEntity> stockData, int stocksNumber);
    }
}
