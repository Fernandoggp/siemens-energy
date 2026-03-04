using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Livro;

namespace Project.Application.UseCases.Livro
{
    public class GetFilteredLivrosUseCase :UseCaseBase, IGetFilteredLivrosUseCase
    {
        private readonly ILivroService _livroService;

        public GetFilteredLivrosUseCase(INotifier notifier, ILogger<GetFilteredLivrosUseCase> logger, ILivroService livroService) : base(notifier, logger)
        {
            _livroService = livroService;
        }

        public async Task<Result> ExecuteAsync(Guid? livroId, Guid? autorId, Guid? generoId)
        {
            return await _livroService.GetFilteredLivrosAsync(livroId, autorId, generoId);
        }
    }
}
