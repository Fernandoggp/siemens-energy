using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Genero;

namespace Project.Application.UseCases.Genero
{
    public class UpdateGeneroUseCase : UseCaseBase, IUpdateGeneroUseCase
    {
        private readonly IGeneroService _generoService;

        public UpdateGeneroUseCase(INotifier notifier, ILogger<UpdateGeneroUseCase> logger, IGeneroService generoService) : base(notifier, logger)
        {
            _generoService = generoService;
        }

        public async Task<Result> ExecuteAsync(GeneroEntity Genero)
        {
            var validationResult = await _generoService.ValidateNameAsync(Genero.Name);

            if (!validationResult.Success)
                return validationResult;

            return await _generoService.UpdateGeneroAsync(Genero);
        }

    }
}
