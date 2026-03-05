using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Services;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;

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

            var livro = await _livroService.GetFilteredLivrosAsync(null, null, id);
            if (livro != null)
            {
                return Result.Fail("Existem livros relacionados a esse genero", System.Net.HttpStatusCode.BadRequest);
            }

            return await _generoService.DeleteGeneroByIdAsync(id);
        }

    }
}
