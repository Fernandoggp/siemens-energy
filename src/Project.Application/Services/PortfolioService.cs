using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.Services;

namespace Project.Application.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepository;

        public PortfolioService(IPortfolioRepository portfolioRepository)
        {
            _portfolioRepository = portfolioRepository;
        }

        public async Task<dynamic> CreateAssetAsync(AssetEntity asset)
        {
            asset = await _portfolioRepository.CreateAssetAsync(asset);
            return asset;
        }

        public async Task<dynamic> GetAssetByUserAsync(string userId)
        {
            List<Object> assets = await _portfolioRepository.GetAssetByUserIdAsync(userId);
            return assets;
        }

        public async Task<dynamic> UpdateAssetAsync(AssetEntity asset)
        {
            asset = await _portfolioRepository.UpdateAssetAsync(asset);
            return asset;
        }

        public async Task<dynamic> GetAssetByIdAsync(Guid id)
        {
            Object asset = await _portfolioRepository.GetAssetByIdAsync(id);
            return asset;
        }

        public async Task DeleteAssetByIdAsync(Guid id)
        {
            await _portfolioRepository.DeleteAssetByIdAsync(id);
        }
    }
}
