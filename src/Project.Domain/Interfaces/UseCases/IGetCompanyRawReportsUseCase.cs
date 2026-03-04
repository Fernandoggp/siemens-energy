namespace Project.Domain.Interfaces.UseCases
{
    public interface IGetCompanyRawReportsUseCase
    {
        Task<dynamic> ExecuteAsync(string ticker);
    }
}
