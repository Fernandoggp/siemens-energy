using Deviot.Common;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Services;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Livro;
using System.Net;

namespace Project.Application.UseCases.Livro
{
    public class UpdateLivroUseCase : UseCaseBase, IUpdateLivroUseCase
    {
        private readonly ILivroService _livroService;
        private readonly IAutorService _autorService;
        private readonly IGeneroService _generoService;

        public UpdateLivroUseCase(INotifier notifier, ILogger<UpdateLivroUseCase> logger, ILivroService livroService, IAutorService autorService, IGeneroService generoService) : base(notifier, logger)
        {
            _livroService = livroService;
            _autorService = autorService;
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync(LivroEntity livro)
        {
            var validationName = await _livroService.ValidateNameAsync(livro.Name, livro.Id);

            if (!validationName.Success)
                return validationName;

            var autor = await _autorService.GetAutorByIdAsync(livro.AutorId);
            if (!autor.Success)
                return autor;

            var genero = await _generoService.GetGeneroByIdAsync(livro.GeneroId);
            if (!genero.Success)
                return genero;

            return await _livroService.UpdateLivroAsync(livro);
        }

    }
}
