using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Dtos;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class GetAssetsUseCase : UseCaseBase, IGetAssetsUseCase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IUserService _userService;

        public GetAssetsUseCase(INotifier notifier, ILogger<GetAssetsUseCase> logger, IPortfolioService portfolioService, IUserService userService) : base(notifier, logger)
        {
            _portfolioService = portfolioService;
            _userService = userService;
        }

        public async Task<dynamic> ExecuteAsync(string userId)
        {
            var user = await _userService.GetUserByIdAsync(userId)
                         ?? throw new ObjectValidationException("Usuário não encontrado");

            var assetsUser = await _portfolioService.GetAssetByUserAsync(userId);

            return assetsUser;
        }
    }
}
