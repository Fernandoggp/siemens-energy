using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Dtos;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    internal class DeleteAssetUseCase : UseCaseBase, IDeleteAssetUseCase
    {
        private readonly IPortfolioService _portfolioService;

        public DeleteAssetUseCase(INotifier notifier, ILogger<DeleteAssetUseCase> logger, IPortfolioService portfolioService) : base(notifier, logger)
        {
            _portfolioService = portfolioService;
        }

        public async Task ExecuteAsync(Guid id)
        {
            var assetUser = await _portfolioService.GetAssetByIdAsync(id)
                             ?? throw new ObjectValidationException("Ativo não encontrado");

            await _portfolioService.DeleteAssetByIdAsync(id);       
        }
    }
}
