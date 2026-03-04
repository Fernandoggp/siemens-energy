using Deviot.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Api.Bases;
using Project.Api.Mappings;
using Project.Api.ViewModel.Dto.Livro;
using Project.Domain.Interfaces.UseCases.Livro;

namespace Project.Api.Controllers.V1
{
    [Route("api/v{version:apiVersion}/livro")]
    [ApiController]
    public class LivroController : CustomControllerBase
    {
        private readonly ICreateLivroUseCase _createLivroUseCase;
        private readonly IGetAllLivrosUseCase _getAllLivrosUseCase;
        private readonly IGetFilteredLivrosUseCase _getFilteredlLivrosUseCase;
        private readonly IUpdateLivroUseCase _updateLivroUseCase;
        private readonly IDeleteLivroByIdUseCase _deleteLivroByIdUseCase;

        public LivroController(INotifier notifier,
                                ILogger<LivroController> logger,
                                ICreateLivroUseCase createLivroUseCase,
                                IGetAllLivrosUseCase getAllLivrosUseCase,
                                IGetFilteredLivrosUseCase getFilteredlLivrosUseCase,
                                IUpdateLivroUseCase updateLivroUseCase,
                                IDeleteLivroByIdUseCase deleteLivroByIdUseCase
            ) : base(notifier, logger)
        {
            _createLivroUseCase = createLivroUseCase;
            _getAllLivrosUseCase = getAllLivrosUseCase;
            _getFilteredlLivrosUseCase = getFilteredlLivrosUseCase;
            _updateLivroUseCase = updateLivroUseCase;
            _deleteLivroByIdUseCase = deleteLivroByIdUseCase;
        }

        [AllowAnonymous]
        [HttpPost("create-livro")]
        public async Task<ActionResult> CreateLivro([FromBody] CreateLivroDto createLivroDto)
        {
            try
            {
                var result = await _createLivroUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapCreateLivroDtoToEntity(createLivroDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("list-livros")]
        public async Task<ActionResult> ListLivros()
        {
            try
            {
                var result = await _getAllLivrosUseCase.ExecuteAsync();

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpGet("filtered-livros")]
        public async Task<ActionResult> FilteredLivros(
            [FromQuery] Guid? livroId = null,
            [FromQuery] Guid? autorId = null,
            [FromQuery] Guid? generoId = null)
        {
            try
            {
                var result = await _getFilteredlLivrosUseCase.ExecuteAsync(livroId, autorId, generoId);

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpPut("update-livro")]
        public async Task<ActionResult> UpdateLivro([FromBody] UpdateLivroDto updateLivroDto)
        {
            try
            {
                var result = await _updateLivroUseCase.ExecuteAsync(
                    ViewModelToEntityMapping.MapUpdateLivroDtoToEntity(updateLivroDto)
                );

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }

        [AllowAnonymous]
        [HttpDelete("delete-livro/{id:guid}")]
        public async Task<ActionResult> DeleteLivro(Guid id)
        {
            try
            {
                var result = await _deleteLivroByIdUseCase.ExecuteAsync(id);

                return CustomResponse(result);
            }
            catch (Exception exception)
            {
                return ReturnActionResultForGenericError(exception);
            }
        }
    }
}
