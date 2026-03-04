using Asp.Versioning;
using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Application.Dtos;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.ComponentModel.DataAnnotations;

namespace Project.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fixed-income")]
    public class FixedIncomeController : CustomControllerBase
    {
        private readonly IFixedIncomeCalculatorUseCase _fixedIncomeCalculatorUseCase;
        private readonly ICompoundInterestCalculatorUseCase _compoundInterestCalculatorUseCase;

        public FixedIncomeController(INotifier notifier,
                                ILogger<FixedIncomeController> logger
                                , IFixedIncomeCalculatorUseCase fixedIncomeCalculatorUseCase
                                , ICompoundInterestCalculatorUseCase compoundInterestCalculatorUseCase
                                ) : base(notifier, logger)
        {
            _fixedIncomeCalculatorUseCase = fixedIncomeCalculatorUseCase;
            _compoundInterestCalculatorUseCase = compoundInterestCalculatorUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("calculator")]
        public async Task<ActionResult> ComparatorCalculator([FromBody] FixedIncomeCalculatorDto dto)
        {
            try
            {
                var calculatorData = await _fixedIncomeCalculatorUseCase.ExecuteAsync(ViewModelToEntityMapping.MapFixedIncomeCalculatorToEntity(dto));
                return CustomResponse(calculatorData);
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
        [HttpGet("compound-interest-calculator")]
        public async Task<ActionResult> CompoundInterestCalculator([FromQuery][Required] float inicialValue, [Required] float monthlyValue, [Required] float profitability, [Required] bool profitabilityIsAnnual, [Required] int time, [Required] bool timeIsAnnual, float desiredValue)
        {
            try
            {
                var calculatorData = await _compoundInterestCalculatorUseCase.ExecuteAsync(inicialValue, monthlyValue, profitability, profitabilityIsAnnual, time, timeIsAnnual, desiredValue);
                return CustomResponse(calculatorData);
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
