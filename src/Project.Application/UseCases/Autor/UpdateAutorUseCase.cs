using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Services.Autor;
using Project.Domain.Interfaces.UseCases.Autor;

namespace Project.Application.UseCases.Autor
{
    public class UpdateAutorUseCase : UseCaseBase, IUpdateAutorUseCase
    {
        private readonly IAutorService _autorService;

        public UpdateAutorUseCase(INotifier notifier, ILogger<UpdateAutorUseCase> logger, IAutorService autorService) : base(notifier, logger)
        {
            _autorService = autorService;
        }

        public async Task<Result> ExecuteAsync(AutorEntity Autor)
        {
            var validationResult = await _autorService.ValidateNameAsync(Autor.Name);

            if (!validationResult.Success)
                return validationResult;

            return await _autorService.UpdateAutorAsync(Autor);
        }

    }
}
