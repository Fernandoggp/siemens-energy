using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services.Autor;
using Project.Domain.Interfaces.UseCases.Autor;

namespace Project.Application.UseCases.Autor
{
    public class DeleteAutorByIdUseCase : UseCaseBase, IDeleteAutorByIdUseCase
    {
        private readonly IAutorService _autorService;

        public DeleteAutorByIdUseCase(INotifier notifier, ILogger<DeleteAutorByIdUseCase> logger, IAutorService autorService) : base(notifier, logger)
        {
            _autorService = autorService;
        }

        public async Task<Result> ExecuteAsync(Guid id)
        {
            return await _autorService.DeleteAutorByIdAsync(id);
        }

    }
}
