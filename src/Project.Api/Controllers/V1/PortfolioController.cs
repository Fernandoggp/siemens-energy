using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Mappings;
using Project.Api.Bases;
using Project.Api.ViewModel.Dto.Portfolio;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.Net;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/portfolio")]
    public class PortfolioController : CustomControllerBase
    {
        private readonly IStockPortfolioReportUseCase _stockPortfolioReportUseCase;
        private readonly ICreateAssetUseCase _createAssetUseCase;
        private readonly IUpdateAssetUseCase _updateAssetUseCase;
        private readonly IGetAssetsUseCase _getAssetsUseCase;
        private readonly IDeleteAssetUseCase _deleteAssetUseCase;

        public PortfolioController(INotifier notifier,
                                ILogger<PortfolioController> logger
                                , IStockPortfolioReportUseCase stockPortfolioReportUseCase
                                , ICreateAssetUseCase createAssetUseCase
                                , IUpdateAssetUseCase updateAssetUseCase
                                , IGetAssetsUseCase getAssetsUseCase
                                , IDeleteAssetUseCase deleteAssetUseCase
                                ) : base(notifier, logger)
        {
            _stockPortfolioReportUseCase = stockPortfolioReportUseCase;
            _createAssetUseCase = createAssetUseCase;
            _updateAssetUseCase = updateAssetUseCase;
            _getAssetsUseCase = getAssetsUseCase;
            _deleteAssetUseCase = deleteAssetUseCase;
        }

        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockPortfolioReportResponseModel))]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("stock-portfolio-report")]
        public async Task<ActionResult> StockPortfolioReport([FromBody()] StockPortfolioReportDto dto)
        {
            try
            {
                if (dto.StocksNumber != 0)
                {
                    var stockPortfolioReportData = await _stockPortfolioReportUseCase.ExecuteAsync(ViewModelToEntityMapping.MapStockPortfolioReportToEntity(dto), dto.StocksNumber);
                    return CustomResponse(stockPortfolioReportData);
                }

                return CustomResponse();
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
        [HttpPost("create-asset")]
        public async Task<ActionResult> CreateAsset([FromBody] CreateAssetDto createAssetDto)
        {
            try
            {
                await _createAssetUseCase.ExecuteAsync(ViewModelToEntityMapping.MapCreateAssetDtoToEntity(createAssetDto));
                return CustomResponse(null, null, HttpStatusCode.OK, new List<string> { "Ativo criado com sucesso" });
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
        [HttpPut("update-asset")]
        public async Task<ActionResult> UpdateAsset([FromBody] UpdateAssetDto updateAssetDto)
        {
            try
            {
                await _updateAssetUseCase.ExecuteAsync(ViewModelToEntityMapping.MapUpdateAssetDtoToEntity(updateAssetDto));
                return CustomResponse(null, null, HttpStatusCode.OK, new List<string> { "Ativo atualizado com sucesso" });
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

        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpGet("assets/{userId}")]
        public async Task<ActionResult> GetAssets(string userId)
        {
            try
            {
                var assets = await _getAssetsUseCase.ExecuteAsync(userId);
                return CustomResponse(assets);
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
        [HttpDelete("assets/{id}")]
        public async Task<ActionResult> DeleteAsset(Guid id)
        {
            try
            {
                await _deleteAssetUseCase.ExecuteAsync(id);
                return CustomResponse(null, null, HttpStatusCode.OK, new List<string> { "Ativo excluído com sucesso" });
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
