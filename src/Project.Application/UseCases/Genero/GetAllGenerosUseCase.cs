using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;
namespace Project.Application.UseCases.Genero
{
    public class GetAllGenerosUseCase :UseCaseBase, IGetAllGenerosUseCase
    {
        private readonly IGeneroService _generoService;

        public GetAllGenerosUseCase(INotifier notifier, ILogger<GetAllGenerosUseCase> logger, IGeneroService generoService) : base(notifier, logger)
        {
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync()
        {
            return await _generoService.GetAllGenerosAsync();
        }
    }
}
