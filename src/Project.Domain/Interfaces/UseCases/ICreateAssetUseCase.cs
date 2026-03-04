using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface ICreateAssetUseCase
    {
        Task<dynamic> ExecuteAsync(AssetEntity asset);
    }
}
