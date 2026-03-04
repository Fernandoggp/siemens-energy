using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.Application.UseCases.Genero
{
    public class DeleteGeneroByIdUseCase : UseCaseBase, IDeleteGeneroByIdUseCase
    {
        private readonly IGeneroService _generoService;

        public DeleteGeneroByIdUseCase(INotifier notifier, ILogger<DeleteGeneroByIdUseCase> logger, IGeneroService generoService) : base(notifier, logger)
        {
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync(Guid id)
        {
            return await _generoService.DeleteGeneroByIdAsync(id);
        }

    }
}
