using Azure;
using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Dtos;
using Project.Domain.Interfaces.Http;
using Project.Application.Base;
using Project.Domain.Entities;
using System.Text.Json;
using Project.Domain.Interfaces.UseCases;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Models;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace Project.Application.UseCases
{
    public class StockPortfolioReportUseCase : UseCaseBase, IStockPortfolioReportUseCase
    {
        private readonly IHttpClientService _httpClientService;
        private readonly ISectorRepository _sectorRepository;

        public StockPortfolioReportUseCase(INotifier notifier, ILogger<StockPortfolioReportUseCase> logger, IHttpClientService httpClientService, ISectorRepository sectorRepository) : base(notifier, logger)
        {
            _httpClientService = httpClientService;
            _sectorRepository = sectorRepository;
        }

        public async Task<dynamic> ExecuteAsync(List<SimpleStockEntity> stockData, int stocksNumber)
        {
            try
            {
                // Fetch data for the first stock
                var request = await _httpClientService.GetAsync($"https://brapi.dev/api/quote/{stockData.FirstOrDefault().Code}?modules=summaryProfile&token=pyn6Ky5bQPd9MYVeJMry3r", null);

                if (string.IsNullOrEmpty(request))
                {
                    throw new Exception("Response from the API is null or empty.");
                }

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                ApiResponse result = JsonSerializer.Deserialize<ApiResponse>(request, options);

                if (result == null || result.Results == null)
                {
                    throw new Exception("Failed to deserialize the response.");
                }

                List<string> sectors = new List<string>();
                decimal totalPortfolioValue = stockData.Sum(stock => stock.Value);

                decimal smallCapsValue = 0;
                decimal midCapsValue = 0;
                decimal blueChipsValue = 0;

                foreach (var stock in result.Results)
                {
                    var stockEntity = stockData.FirstOrDefault(s => s.Code == stock?.Name); 
                    if (stockEntity == null)
                    {
                        continue; 
                    }

                    var name = stock?.Name;
                    var sector = stock?.SummaryProfile?.Sector;
                    var evValue = stock?.DefaultKeyStatistics?.EnterpriseValue;

                    sectors.Add(sector);

                    if (evValue <= 100000000000)
                    {
                        smallCapsValue += stockEntity.Value;
                    }
                    else if (evValue > 100000000000 && evValue <= 500000000000)
                    {
                        midCapsValue += stockEntity.Value;
                    }
                    else
                    {
                        blueChipsValue += stockEntity.Value;
                    }
                }

                double smallCapsPercentage = (double)(smallCapsValue / totalPortfolioValue) * 100;
                double midCapsPercentage = (double)(midCapsValue / totalPortfolioValue) * 100;
                double blueChipsPercentage = (double)(blueChipsValue / totalPortfolioValue) * 100;

                var sectorCounts = sectors.GroupBy(s => s)
                                          .ToDictionary(g => g.Key, g => g.Count());

                List<SectorModel> sectorsInfo = new List<SectorModel>();

                var dbSectors = await _sectorRepository.ExecuteAsync() as List<SectorDbModel>;
                var sectorsMapping = dbSectors.ToDictionary(s => s.Name.ToLower(), s => s.Perennial);

                foreach (var stock in stockData)
                {
                    var apiStock = result.Results.FirstOrDefault(r => r.Name == stock.Code);
                    if (apiStock != null)
                    {
                        var sector = apiStock.SummaryProfile.Sector;
                        if (sectorsMapping.TryGetValue(sector.ToLower(), out string perennialStatus))
                        {
                            sectorsInfo.Add(new SectorModel
                            {
                                Name = sector,
                                Perennial = perennialStatus,
                                Percentage = (double)(stock.Value / totalPortfolioValue) * 100
                            });
                        }
                        else
                        {
                            sectorsInfo.Add(new SectorModel
                            {
                                Name = sector,
                                Perennial = "unknown",
                                Percentage = (double)(stock.Value / totalPortfolioValue) * 100
                            });
                        }
                    }
                }

                int distinctItems = sectorsInfo.Select(s => s.Name).Distinct().Count();

                var (weakPoints, warningPoints, strengths) = await PortfolioAlerts(
                    stocksNumber,
                    smallCapsPercentage,
                    midCapsPercentage,
                    blueChipsPercentage,
                    distinctItems,
                    sectorsInfo
                );

                return new
                {
                    WeakPoints = weakPoints,
                    WarningPoints = warningPoints,
                    Strengths = strengths
                };
            }
            catch (Exception ex)
            {
                NotifyInternalServerError(ex);
            }
            return null;
        }


        private Task<(List<string> WeakPoints, List<string> WarningPoints, List<string> Strengths)> PortfolioAlerts(
            int stocksNumber,
            double smallCapsPercentage,
            double midCapsPercentage,
            double blueChipsPercentage,
            int distinctItems,
            List<SectorModel> sectorsInfo
        )
        {
            var strengths = new List<string>();
            var warningPoints = new List<string>();
            var weakPoints = new List<string>();

            // stocksNumber
            if (stocksNumber <= 6)
            {
                weakPoints.Add("Alta exposição em poucos ativos");
            }
            else if (stocksNumber > 6 && stocksNumber <= 8)
            {
                warningPoints.Add("Possível perda de boas oportunidades");
                strengths.Add("Número controlado de ações");
            }
            else if (stocksNumber > 8 && stocksNumber <= 15)
            {
                strengths.Add("Número controlado de ações");
            }
            else if (stocksNumber > 15 && stocksNumber <= 20)
            {
                warningPoints.Add("Possível descontrole no número de ações");
            }
            else if (stocksNumber > 20)
            {
                weakPoints.Add("Número descontrolado de ações");
            }

            // smallCapsPercentage
            if (smallCapsPercentage >= 50)
            {
                weakPoints.Add("Carteira excessivamente agressiva com alto risco de volatilidade descontrolada");
            }
            else if (smallCapsPercentage >= 30 && smallCapsPercentage < 50)
            {
                weakPoints.Add("Alto risco de volatilidade indevida");
            }
            else if (smallCapsPercentage >= 20 && smallCapsPercentage < 30)
            {
                warningPoints.Add("Risco de volatilidade indevida");
            }
            else if (smallCapsPercentage > 7 && smallCapsPercentage < 20)
            {
                strengths.Add("Boa participação em empresas consolidadas");
            }
            else if (smallCapsPercentage <= 7)
            {
                warningPoints.Add("Considere obter uma participação um pouco maior em small caps consistentes");
                strengths.Add("Boa participação em empresas consolidadas");
            }

            // distinctItems
            if (distinctItems <= 4)
            {
                weakPoints.Add("Baixa diversificação setorial");
            }
            else if (distinctItems > 4 && distinctItems <= 6)
            {
                strengths.Add("Boa diversificação setorial");
            }
            else if (distinctItems > 6 && distinctItems <= 9)
            {
                strengths.Add("Ótima diversificação setorial");
            }
            else if (distinctItems > 9 && distinctItems <= 11)
            {
                warningPoints.Add("Possível posição em setores com risco alto de falências");
            }
            else if (distinctItems > 11)
            {
                weakPoints.Add("Posição em setores com risco alto de falências");
            }

            // sectorsInfo
            foreach (var item in sectorsInfo)
            {
                if (item.Perennial == "unstable" && item.Percentage >= 50)
                {
                    weakPoints.Add("Grande parte da carteira em setores muito instáveis");
                    warningPoints.Add("Tenha participação maior em setores perenes");
                }
                else if (item.Perennial == "intermediary" && item.Percentage >= 20)
                {
                    warningPoints.Add("Tenha participação maior em setores perenes");
                }
                else if (item.Perennial == "perennial" && item.Percentage >= 50)
                {
                    strengths.Add("Grande parte da carteira em setores estáveis");
                }
            }

            return Task.FromResult((weakPoints, warningPoints, strengths));
        }
    }
}
