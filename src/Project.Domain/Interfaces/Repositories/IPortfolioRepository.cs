using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Repositories
{
    public interface IPortfolioRepository
    {
        Task<dynamic> GetAssetByUserIdAsync(string userId);
        Task<dynamic> CreateAssetAsync(AssetEntity asset);
        Task<dynamic> UpdateAssetAsync(AssetEntity asset);
        Task<dynamic> GetAssetByIdAsync(Guid id);
        Task DeleteAssetByIdAsync(Guid id);
    }
}
