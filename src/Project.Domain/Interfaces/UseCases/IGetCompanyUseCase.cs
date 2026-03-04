namespace Project.Domain.Interfaces.UseCases
{
    public interface IGetCompanyUseCase
    {
        Task<dynamic> ExecuteAsync(string ticker);
    }
}
