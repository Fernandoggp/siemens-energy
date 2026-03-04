using Deviot.Common;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Login;
using Project.Api.ViewModel.Dto.User;
using Project.Application.UseCases;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.Net;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/login")]
    public class LoginController : CustomControllerBase
    {
        private readonly ILoginUseCase _loginUseCase;

        public LoginController(INotifier notifier, ILogger<LoginController> logger, ILoginUseCase loginUseCase) : base(notifier, logger)
        {
            _loginUseCase = loginUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var login = await _loginUseCase.ExecuteAsync(ViewModelToEntityMapping.MapLoginDtoToEntity(loginDto));
                return CustomResponse(login);
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
