using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Base;
using Project.Repository.Builders.Portfolio;
using Project.Repository.Core;

namespace Project.Repository.Repositories
{
    public class PortfolioRepository : RepositoryBase, IPortfolioRepository
    {
        private readonly IPortfolioBuilder _portfolioBuilder;

        public PortfolioRepository(IDbService dbService, IPortfolioBuilder portfolioBuilder) : base(dbService)
        {
            _portfolioBuilder = portfolioBuilder;
        }

        public async Task<dynamic> CreateAssetAsync(AssetEntity asset)
        {
            var request = _portfolioBuilder.CreateAsset(asset);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task<dynamic> GetAssetByUserIdAsync(string userId)
        {
            var request = _portfolioBuilder.GetAssetByUserId(userId);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task<dynamic> UpdateAssetAsync(AssetEntity asset)
        {
            var request = _portfolioBuilder.UpdateAsset(asset);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task<dynamic> GetAssetByIdAsync(Guid id)
        {
            var request = _portfolioBuilder.GetAssetById(id);
            var results = await _dbService.ExecuteQueryRequestAsync<dynamic>(request);

            return results;
        }

        public async Task DeleteAssetByIdAsync(Guid id)
        {
            var request = _portfolioBuilder.DeleteAssetById(id);
            await _dbService.ExecuteQueryRequestAsync<dynamic>(request);
        }
    }
}
