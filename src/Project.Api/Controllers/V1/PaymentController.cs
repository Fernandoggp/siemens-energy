using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Payment;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.Net;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/payment")]
    public class PaymentController : CustomControllerBase
    {
        private readonly ICreateSignatureUseCase _createSignatureUseCase; 

        public PaymentController(INotifier notifier,
                                ILogger<PaymentController> logger,
                                ICreateSignatureUseCase createSignatureUseCase
            ) : base(notifier, logger)
        {
            _createSignatureUseCase = createSignatureUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("create-signature")]
        public async Task<ActionResult> CreateUser([FromBody] CreateSignatureDto createSignatureDto)
        {
            try
            {
                await _createSignatureUseCase.ExecuteAsync(ViewModelToEntityMapping.MapSignatureDtoToEntity(createSignatureDto));
                return CustomResponse(null, null, HttpStatusCode.OK, new List<string> { "Assinatura criada com sucesso" });
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
