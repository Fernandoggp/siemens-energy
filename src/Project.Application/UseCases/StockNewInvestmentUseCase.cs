using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class StockNewInvestmentUseCase : UseCaseBase, IStockNewInvestmentUseCase
    {
        private readonly IStockService _stockService;

        public StockNewInvestmentUseCase(INotifier notifier, ILogger<StockNewInvestmentUseCase> logger, IStockService stockService) : base(notifier, logger)
        {
            _stockService = stockService;
        }

        public async Task<dynamic> ExecuteAsync(List<StockNewInvestmentEntity> stocks, decimal value)
        {
            Object stocksResult = await _stockService.CalculateNewInvestmentAsync(stocks, value);
            return stocksResult;
        }
    }
}
