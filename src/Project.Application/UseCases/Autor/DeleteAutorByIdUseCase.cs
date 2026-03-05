using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Autor;

namespace Project.Application.UseCases.Autor
{
    public class DeleteAutorByIdUseCase : UseCaseBase, IDeleteAutorByIdUseCase
    {
        private readonly IAutorService _autorService;
        private readonly ILivroService _livroService;

        public DeleteAutorByIdUseCase(INotifier notifier, ILogger<DeleteAutorByIdUseCase> logger, IAutorService autorService, ILivroService livroService) : base(notifier, logger)
        {
            _autorService = autorService;
            _livroService = livroService;
        }

        public async Task<Result> ExecuteAsync(Guid id)
        {
            var autor = await _autorService.GetAutorByIdAsync(id);
            if (!autor.Success)
            {
                return Result.Fail(autor.Message, autor.StatusCode);
            }

            var livro = await _livroService.GetFilteredLivrosAsync(null, id, null);
            if (livro != null) { 
                return Result.Fail("Existem livros relacionados a esse autor", System.Net.HttpStatusCode.BadRequest);
            }

            return await _autorService.DeleteAutorByIdAsync(id);
        }

    }
}
