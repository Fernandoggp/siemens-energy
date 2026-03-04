namespace Project.Domain.Interfaces.UseCases
{
    public interface ICompoundInterestCalculatorUseCase
    {
        Task<dynamic> ExecuteAsync(float inicialValue, float monthlyValue, float profitability, bool profitabilityIsAnnual, int time, bool TimeIsAnnual, float desiredValue);
    }
}
