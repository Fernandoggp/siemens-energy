using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface IPortfolioService
    {
        Task<dynamic> GetAssetByUserAsync(string userId);
        Task<dynamic> CreateAssetAsync(AssetEntity asset);
        Task<dynamic> UpdateAssetAsync(AssetEntity asset);
        Task<dynamic> GetAssetByIdAsync(Guid id);
        Task DeleteAssetByIdAsync(Guid id);
    }
}
