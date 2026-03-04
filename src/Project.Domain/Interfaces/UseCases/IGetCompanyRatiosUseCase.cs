namespace Project.Domain.Interfaces.UseCases
{
    public interface IGetCompanyRatiosUseCase
    {
        Task<dynamic> ExecuteAsync(string ticker);
    }
}
