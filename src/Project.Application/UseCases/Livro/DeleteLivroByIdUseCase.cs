using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Common;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases.Livro;

namespace Project.Application.UseCases.Livro
{
    public class DeleteLivroByIdUseCase : UseCaseBase, IDeleteLivroByIdUseCase
    {
        private readonly ILivroService _livroService;

        public DeleteLivroByIdUseCase(INotifier notifier, ILogger<DeleteLivroByIdUseCase> logger, ILivroService livroService) : base(notifier, logger)
        {
            _livroService = livroService;
        }

        public async Task<Result> ExecuteAsync(Guid id)
        {
            return await _livroService.DeleteLivroByIdAsync(id);
        }

    }
}
