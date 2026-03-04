using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface IFixedIncomeCalculatorUseCase
    {
        Task<string> ExecuteAsync(List<FixedIncomeCalculatorEntity> investments);
    }
}
