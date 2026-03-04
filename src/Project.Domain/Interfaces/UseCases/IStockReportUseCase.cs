namespace Project.Domain.Interfaces.UseCases
{
    public interface IStockReportUseCase
    {
        Task<dynamic> ExecuteAsync(string code);
    }
}
