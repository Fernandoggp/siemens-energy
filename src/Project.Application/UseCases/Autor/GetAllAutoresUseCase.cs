using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Autor;
namespace Project.Application.UseCases.Autor
{
    public class GetAllAutoresUseCase :UseCaseBase, IGetAllAutoresUseCase
    {
        private readonly IAutorService _autorService;

        public GetAllAutoresUseCase(INotifier notifier, ILogger<GetAllAutoresUseCase> logger, IAutorService autorService) : base(notifier, logger)
        {
            _autorService = autorService;
        }

        public async Task<Result> ExecuteAsync()
        {
            return await _autorService.GetAllAutoresAsync();
        }
    }
}
