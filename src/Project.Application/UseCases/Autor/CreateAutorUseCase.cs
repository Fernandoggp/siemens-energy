using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Autor;

namespace Project.Application.UseCases
{
    public class CreateAutorUseCase : UseCaseBase, ICreateAutorUseCase
    {
        private readonly IAutorService _autorService;

        public CreateAutorUseCase(INotifier notifier, ILogger<CreateAutorUseCase> logger, IAutorService autorService) : base(notifier, logger)
        {
            _autorService = autorService;
        }

        public async Task<Result> ExecuteAsync(AutorEntity newAutor)
        {
            var validationResult = await _autorService.ValidateNameAsync(newAutor.Name);

            if (!validationResult.Success)
                return validationResult;

            return await _autorService.CreateAutorAsync(newAutor);
        }

    }
}
