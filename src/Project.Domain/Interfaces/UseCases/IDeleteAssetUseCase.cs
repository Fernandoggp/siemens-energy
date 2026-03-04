namespace Project.Domain.Interfaces.UseCases
{
    public interface IDeleteAssetUseCase
    {
        Task ExecuteAsync(Guid id);
    }
}
