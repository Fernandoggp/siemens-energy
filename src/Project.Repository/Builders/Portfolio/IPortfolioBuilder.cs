using Project.Domain.Entities;
using Project.Repository.Core;

namespace Project.Repository.Builders.Portfolio
{
    public interface IPortfolioBuilder
    {
        Request GetAssetByUserId(string userId);
        Request CreateAsset(AssetEntity asset);
        Request UpdateAsset(AssetEntity asset);
        Request GetAssetById(Guid id);
        Request DeleteAssetById(Guid id);
    }
}
