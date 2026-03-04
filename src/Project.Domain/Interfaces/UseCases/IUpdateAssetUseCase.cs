using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface IUpdateAssetUseCase
    {
        Task<dynamic> ExecuteAsync(AssetEntity asset);
    }
}
