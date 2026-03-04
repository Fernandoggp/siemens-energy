namespace Project.Domain.Interfaces.UseCases
{
    public interface IGetCompanyRatiosValuationUseCase
    {
        Task<dynamic> ExecuteAsync(string ticker);
    }
}
