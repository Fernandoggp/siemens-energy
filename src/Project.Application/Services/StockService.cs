using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;

namespace Project.Application.Services
{
    public class StockService : IStockService
    {
        public async Task<dynamic> CalculateNewInvestmentAsync(List<StockNewInvestmentEntity> stocks, decimal value)
        {
            if (stocks == null || !stocks.Any())
                throw new ObjectValidationException("A lista de ações não pode ser nula ou vazia.");

            decimal totalStockValue = stocks.Sum(s => s.Value);
            decimal totalValue = totalStockValue + value;

            var updatedStocks = stocks.Select(s => new
            {
                Name = s.Name,
                NewValue = Math.Round((totalValue * (s.DesiredPercentage / 100m)) - s.Value, 2)
            }).ToList();

            return await Task.FromResult(updatedStocks);
        }
    }
}
