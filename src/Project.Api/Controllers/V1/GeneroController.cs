using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Genero;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/genero")]
    [ApiController]
    public class GeneroController : CustomControllerBase
    {
        private readonly ICreateGeneroUseCase _createGeneroUseCase;
        private readonly IGetAllGenerosUseCase _getAllGenerosUseCase;
        private readonly IUpdateGeneroUseCase _updateGeneroUseCase;
        private readonly IDeleteGeneroByIdUseCase _deleteGeneroByIdUseCase;

        public GeneroController(INotifier notifier,
                                ILogger<GeneroController> logger,
                                ICreateGeneroUseCase createGeneroUseCase,
                                IGetAllGenerosUseCase getAllGenerosUseCase,
                                IUpdateGeneroUseCase updateGeneroUseCase,
                                IDeleteGeneroByIdUseCase deleteGeneroByIdUseCase
            ) : base(notifier, logger)
        {
            _createGeneroUseCase = createGeneroUseCase;
            _getAllGenerosUseCase = getAllGenerosUseCase;
            _updateGeneroUseCase = updateGeneroUseCase;
            _deleteGeneroByIdUseCase = deleteGeneroByIdUseCase;
        }

        [AllowAnonymous]
        [HttpPost("create-genero")]
        public async Task<ActionResult> CreateGenero([FromBody] CreateGeneroDto createGeneroDto)
        {
            try
            {
                var result = await _createGeneroUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapCreateGeneroDtoToEntity(createGeneroDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("list-generos")]
        public async Task<ActionResult> ListGeneros()
        {
            try
            {
                var result = await _getAllGenerosUseCase.ExecuteAsync();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpPut("update-genero")]
        public async Task<ActionResult> UpdateGenero([FromBody] UpdateGeneroDto updateGeneroDto)
        {
            try
            {
                var result = await _updateGeneroUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapUpdateGeneroDtoToEntity(updateGeneroDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete-genero/{id:guid}")]
        public async Task<ActionResult> DeleteGenero(Guid id)
        {
            try
            {
                var result = await _deleteGeneroByIdUseCase.ExecuteAsync(id);

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
