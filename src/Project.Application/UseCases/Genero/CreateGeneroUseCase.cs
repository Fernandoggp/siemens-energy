using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.Application.UseCases
{
    public class CreateGeneroUseCase : UseCaseBase, ICreateGeneroUseCase
    {
        private readonly IGeneroService _generoService;

        public CreateGeneroUseCase(INotifier notifier, ILogger<CreateGeneroUseCase> logger, IGeneroService generoService) : base(notifier, logger)
        {
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync(GeneroEntity newGenero)
        {
            var validationResult = await _generoService.ValidateNameAsync(newGenero.Name);

            if (!validationResult.Success)
                return validationResult;

            return await _generoService.CreateGeneroAsync(newGenero);
        }

    }
}
