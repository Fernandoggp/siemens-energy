using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Livro;
namespace Project.Application.UseCases.Livro
{
    public class GetAllLivrosUseCase :UseCaseBase, IGetAllLivrosUseCase
    {
        private readonly ILivroService _livroService;

        public GetAllLivrosUseCase(INotifier notifier, ILogger<GetAllLivrosUseCase> logger, ILivroService livroService) : base(notifier, logger)
        {
            _livroService = livroService;
        }

        public async Task<Result> ExecuteAsync()
        {
            return await _livroService.GetAllLivrosAsync();
        }
    }
}
