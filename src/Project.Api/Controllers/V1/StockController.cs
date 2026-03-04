using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Portfolio;
using Project.Api.ViewModel.Dto.Stock;
using Project.Application.UseCases;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/stock")]
    public class StockController : CustomControllerBase
    {
        private readonly IStockReportUseCase _stockReportUseCase;
        private readonly IStockNewInvestmentUseCase _stockNewInvestmentUseCase;

        public StockController(INotifier notifier,
                                ILogger<StockController> logger
                                , IStockReportUseCase stockReportUseCase
                                , IStockNewInvestmentUseCase stockNewInvestmentUseCase
                                ) : base(notifier, logger)
        {
            _stockReportUseCase = stockReportUseCase;
            _stockNewInvestmentUseCase = stockNewInvestmentUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpGet("stock-report")]
        public async Task<ActionResult> StockReport([FromQuery()] [Required] string code)
        {
            try
            {
                var stockReportData = await _stockReportUseCase.ExecuteAsync(code);
                return CustomResponse(stockReportData);
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("new-investment")]
        public async Task<ActionResult> NewInvestment([FromBody] StockNewInvestmentDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("O corpo da requisição está nulo.");
                }

                if (dto.Stocks == null || !dto.Stocks.Any())
                {
                    return BadRequest("A lista de ações não pode estar vazia.");
                }

                var stocksEntities = ViewModelToEntityMapping.MapStockNewInvestmentDtoToEntity(dto);
                var result = await _stockNewInvestmentUseCase.ExecuteAsync(stocksEntities, dto.NewInvestmentValue);

                return CustomResponse(result);
            }
            catch (ObjectValidationException exception)
            {
                return ReturnActionResultForValidationError(exception);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

    }
}
