using Project.Domain.Entities;
using Project.Repository.Core;

namespace Project.Repository.Builders.Portfolio
{
    public partial class PortfolioBuilder : IPortfolioBuilder
    {
        public Request CreateAsset(AssetEntity asset)
        {
            Id = asset.Id;
            Name = asset.Name;
            Value = asset.Value;
            DesiredPercentage = asset.DesiredPercentage;
            UserId = asset.UserId;

            var sql = CreateAssetSql();

            return new Request(sql, null);
        }

        public Request GetAssetByUserId(string userId)
        {
            UserId = userId;
            var sql = GetAssetByUserIdSql();

            return new Request(sql, null);
        }

        public Request UpdateAsset(AssetEntity asset)
        {
            Id = asset.Id;
            Name = asset.Name;
            Value = asset.Value;
            DesiredPercentage = asset.DesiredPercentage;

            var sql = UpdateAssetSql();

            return new Request(sql, null);
        }

        public Request GetAssetById(Guid id)
        {
            Id = id;
            var sql = GetAssetByIdSql();

            return new Request(sql, null);
        }

        public Request DeleteAssetById(Guid id)
        {
            Id = id;
            var sql = DeleteAssetByIdSql();

            return new Request(sql, null);
        }
    }
}
