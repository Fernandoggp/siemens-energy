using Deviot.Common;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Dtos;
using Project.Domain.Interfaces.Http;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Application.UseCases
{
    public class StockReportUseCase : UseCaseBase, IStockReportUseCase
    {
        private readonly IHttpClientService _httpClientService;

        public StockReportUseCase(INotifier notifier, ILogger<StockReportUseCase> logger, IHttpClientService httpClientService) : base(notifier, logger)
        {
            _httpClientService = httpClientService;
        }

        public async Task<dynamic> ExecuteAsync(string code)
        {
            var request = await _httpClientService.GetAsync($"https://brapi.dev/api/quote/{code}?modules=summaryProfile&token=pyn6Ky5bQPd9MYVeJMry3r", null);

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
            return null;

            //Tag along - nao tem no brapi

            //Lucros consistentes

            //Divida liquida / ebtida

            //Setor

            //Margem de crescimento

            //ROE

            //ROIC

            //(Coisas relacionadas ao patrimonio)

            //PL

            //P / VP

            //L / PA

            //Margem liquida

            //DY

            //Tempo de mercado(fundação)

            //Tempo de bolsa(IPO)

            //Free float

            //Governança

            //Ano fiscal(prejuizo boolean)

            //Liderança no segmento(Tamanho)

            //Payout

            //Liquidez

            //EBIT

            //EBITDA
        }
    }
}
