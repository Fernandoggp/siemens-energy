using Deviot.Common;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class CreateAssetUseCase : UseCaseBase, ICreateAssetUseCase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IUserService _userService;

        public CreateAssetUseCase(INotifier notifier, ILogger<CreateAssetUseCase> logger, IPortfolioService portfolioService, IUserService userService) : base(notifier, logger)
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

            var assetsUserDynamic = await _portfolioService.GetAssetByUserAsync(asset.UserId);
            List<int> assetsUser = new List<int>();

            if (assetsUserDynamic != null)
            {
                foreach (var assetUser in assetsUserDynamic)
                {
                    assetsUser.Add((int)assetUser.desired_percentage);
                    if (string.Equals(assetUser.name, asset.Name, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new ObjectValidationException("Ativo já cadastrado");
                    }
                }
            }

            int totalPercentage = assetsUser.Sum() + asset.DesiredPercentage;

            if (totalPercentage > 100)
            {
                throw new ObjectValidationException("A soma das porcentagens dos ativos não pode ultrapassar 100%");
            }

            await _portfolioService.CreateAssetAsync(asset);

            return asset;
        }

    }
}
