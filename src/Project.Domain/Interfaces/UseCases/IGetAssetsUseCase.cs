namespace Project.Domain.Interfaces.UseCases
{
    public interface IGetAssetsUseCase
    {
        Task<dynamic> ExecuteAsync(string userId);
    }
}