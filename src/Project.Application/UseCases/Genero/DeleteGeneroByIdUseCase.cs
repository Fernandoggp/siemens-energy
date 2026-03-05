using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Services;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;
using System.Net;

namespace Project.Application.UseCases.Genero
{
    public class DeleteGeneroByIdUseCase : UseCaseBase, IDeleteGeneroByIdUseCase
    {
        private readonly IGeneroService _generoService;
        private readonly ILivroService _livroService;

        public DeleteGeneroByIdUseCase(INotifier notifier, ILogger<DeleteGeneroByIdUseCase> logger, IGeneroService generoService, ILivroService livroService) : base(notifier, logger)
        {
            _generoService = generoService;
            _livroService = livroService;
        }

        public async Task<Result> ExecuteAsync(Guid id)
        {
            var genero = await _generoService.GetGeneroByIdAsync(id);
            if (!genero.Success)
            {
                return Result.Fail(genero.Message, genero.StatusCode);
            }

            var livroResult = await _livroService.GetFilteredLivrosAsync(null, null, id);

            var livros = livroResult.Data as IEnumerable<object>;

            if (livros != null && livros.Any())
            {
                return Result.Fail("Existem livros relacionados a esse genero", HttpStatusCode.BadRequest);
            }

            return await _generoService.DeleteGeneroByIdAsync(id);
        }

    }
}
