namespace Project.Domain.Interfaces.Services
{
    public interface ICedroService
    {
        Task<dynamic> GetCompaniesAsync();
        Task<dynamic> GetCompanyAsync(string ticker);
        Task<dynamic> GetCompanyRatiosAsync(string ticker);
        Task<dynamic> GetCompanyRatiosValuationAsync(string ticker);
        Task<dynamic> GetCompanyRawReportsAsync(string ticker);
    }
}
