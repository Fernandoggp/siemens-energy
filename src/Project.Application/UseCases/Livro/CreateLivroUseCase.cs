using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Livro;

namespace Project.Application.UseCases
{
    public class CreateLivroUseCase : UseCaseBase, ICreateLivroUseCase
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;
        private readonly IGeneroService _generoService;

        public CreateLivroUseCase(INotifier notifier, ILogger<CreateLivroUseCase> logger, ILivroService livroService, IAutorService autorService, IGeneroService generoService) : base(notifier, logger)
        {
            _livroService = livroService;
            _autorService = autorService;
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync(LivroEntity newLivro)
        {
            var validationName = await _livroService.ValidateNameAsync(newLivro.Name);

            if (!validationName.Success)
                return validationName;

            var autor = await _autorService.GetAutorByIdAsync(newLivro.AutorId);
            if (!autor.Success)
                return autor;

            var genero = await _generoService.GetGeneroByIdAsync(newLivro.GeneroId);
            if (!genero.Success)
                return genero;

            return await _livroService.CreateLivroAsync(newLivro);
        }

    }
}
