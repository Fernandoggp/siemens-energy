using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface ILoginUseCase
    {
        Task<dynamic> ExecuteAsync(LoginEntity login);
    }
}
