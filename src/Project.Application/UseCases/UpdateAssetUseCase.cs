using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class UpdateAssetUseCase : UseCaseBase, IUpdateAssetUseCase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IUserService _userService;

        public UpdateAssetUseCase(INotifier notifier, ILogger<UpdateAssetUseCase> logger, IPortfolioService portfolioService, IUserService userService) : base(notifier, logger)
        {
            _portfolioService = portfolioService;
            _userService = userService;
        }

        public async Task<dynamic> ExecuteAsync(AssetEntity asset)
        {
            if (asset.Value <= 0)
            {
                throw new ObjectValidationException("Valor não pode ser menor ou igual a zero");
            }

            var user = await _userService.GetUserByIdAsync(asset.UserId)
                         ?? throw new ObjectValidationException("Usuário não encontrado");

            var assetUser = await _portfolioService.GetAssetByIdAsync(asset.Id)
                             ?? throw new ObjectValidationException("Ativo não encontrado");

            var assetsUserDynamic = await _portfolioService.GetAssetByUserAsync(asset.UserId);

            foreach (var a in assetsUserDynamic)
            {
                if (string.Equals(a.name, asset.Name, StringComparison.OrdinalIgnoreCase) &&
                    a.id != asset.Id)
                {
                    throw new ObjectValidationException("Ativo já cadastrado");
                }
            }

            List<int> assetsUser = new List<int>();
            foreach (var assetUserList in assetsUserDynamic)
            {
                assetsUser.Add((int)assetUserList.desired_percentage);
            }

            int totalPercentage = assetsUser.Sum() + asset.DesiredPercentage;

            asset.Name ??= assetUser.Name;
            asset.Value = asset.Value != 0 ? asset.Value : assetUser.Value;
            asset.DesiredPercentage = asset.DesiredPercentage != 0 ? asset.DesiredPercentage : 0;

            await _portfolioService.UpdateAssetAsync(asset);

            return asset;
        }

    }
}
