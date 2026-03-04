using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Autor;
using Project.Domain.Interfaces.UseCases.Autor;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/autor")]
    [ApiController]
    public class AutorController: CustomControllerBase
    {
        private readonly ICreateAutorUseCase _createAutorUseCase;
        private readonly IGetAllAutoresUseCase _getAllAutoresUseCase;
        private readonly IUpdateAutorUseCase _updateAutorUseCase;
        private readonly IDeleteAutorByIdUseCase _deleteAutorByIdUseCase;

        public AutorController(INotifier notifier,
                                ILogger<AutorController> logger,
                                ICreateAutorUseCase createAutorUseCase,
                                IGetAllAutoresUseCase getAllAutoresUseCase,
                                IUpdateAutorUseCase updateAutorUseCase,
                                IDeleteAutorByIdUseCase deleteAutorByIdUseCase
            ) : base(notifier, logger)
        {
            _createAutorUseCase = createAutorUseCase;
            _getAllAutoresUseCase = getAllAutoresUseCase;
            _updateAutorUseCase = updateAutorUseCase;
            _deleteAutorByIdUseCase = deleteAutorByIdUseCase;
        }

        [AllowAnonymous]
        [HttpPost("create-autor")]
        public async Task<ActionResult> CreateAutor([FromBody] CreateAutorDto createAutorDto)
        {
            try
            {
                var result = await _createAutorUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapCreateAutorDtoToEntity(createAutorDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("list-autores")]
        public async Task<ActionResult> ListAutores()
        {
            try
            {
                var result = await _getAllAutoresUseCase.ExecuteAsync();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpPut("update-autor")]
        public async Task<ActionResult> UpdateAutor([FromBody] UpdateAutorDto updateAutorDto)
        {
            try
            {
                var result = await _updateAutorUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapUpdateAutorDtoToEntity(updateAutorDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete-autor/{id:guid}")]
        public async Task<ActionResult> DeleteAutor(Guid id)
        {
            try
            {
                var result = await _deleteAutorByIdUseCase.ExecuteAsync(id);

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
