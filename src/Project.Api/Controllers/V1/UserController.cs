using Deviot.Common;
using DocumentFormat.OpenXml.Math;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.User;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.UseCases;
using System.Net;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/user")]
    public class UserController: CustomControllerBase
    {
        private readonly ICreateUserUseCase _createUserUseCase;

        public UserController(INotifier notifier,
                                ILogger<UserController> logger,
                                ICreateUserUseCase createUserUseCase
            ) : base(notifier, logger)
        {
            _createUserUseCase = createUserUseCase;
        }

        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomActionResult))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CustomActionResult))]
        [HttpPost("create-user")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            try
            {
                await _createUserUseCase.ExecuteAsync(ViewModelToEntityMapping.MapUserDtoToEntity(createUserDto));
                return CustomResponse(null, null, HttpStatusCode.OK, new List<string> { "Usuário criado com sucesso" });
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
