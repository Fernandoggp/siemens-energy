using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/cedro")]
    public class CedroController : CustomControllerBase
    {
        private readonly IListCompaniesUseCase _listCompaniesUseCase;
        private readonly IGetCompanyUseCase _getCompanyUseCase;
        private readonly IGetCompanyRatiosUseCase _getCompanyRatiosUseCase;
        private readonly IGetCompanyRatiosValuationUseCase _getCompanyRatiosValuationUseCase;
        private readonly IGetCompanyRawReportsUseCase _getCompanyRawReportsUseCase;

        public CedroController(INotifier notifier,
                                ILogger<CedroController> logger,
                                IListCompaniesUseCase listCompaniesUseCase,
                                IGetCompanyUseCase getCompanyUseCase,
                                IGetCompanyRatiosUseCase getCompanyRatiosUseCase,
                                IGetCompanyRatiosValuationUseCase getCompanyRatiosValuationUseCase,
                                IGetCompanyRawReportsUseCase getCompanyRawReportsUseCase
            ) : base(notifier, logger)
        {
            _listCompaniesUseCase = listCompaniesUseCase;
            _getCompanyUseCase = getCompanyUseCase;
            _getCompanyRatiosUseCase = getCompanyRatiosUseCase;
            _getCompanyRatiosValuationUseCase = getCompanyRatiosValuationUseCase;
            _getCompanyRawReportsUseCase = getCompanyRawReportsUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpGet("list-companies")]
        public async Task<ActionResult> ListCompanies()
        {
            try
            {
                var companies = await _listCompaniesUseCase.ExecuteAsync();
                return CustomResponse(companies);
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
        [HttpGet("company/{ticker}")]
        public async Task<ActionResult> CompanyByTicker([FromRoute][Required] string ticker)
        {
            try
            {
                var company = await _getCompanyUseCase.ExecuteAsync(ticker);
                return CustomResponse(company);
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
        [HttpGet("company-ratios/{ticker}")]
        public async Task<ActionResult> CompanyRatios([FromRoute][Required] string ticker)
        {
            try
            {
                var company = await _getCompanyRatiosUseCase.ExecuteAsync(ticker);
                return CustomResponse(company);
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
        [HttpGet("company-ratios-valuation/{ticker}")]
        public async Task<ActionResult> CompanyRatiosValuation([FromRoute][Required] string ticker)
        {
            try
            {
                var company = await _getCompanyRatiosValuationUseCase.ExecuteAsync(ticker);
                return CustomResponse(company);
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
        [HttpGet("company-raw-reports/{ticker}")]
        public async Task<ActionResult> CompanyRawReports([FromRoute][Required] string ticker)
        {
            try
            {
                var company = await _getCompanyRawReportsUseCase.ExecuteAsync(ticker);
                return CustomResponse(company);
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
