using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface ICreateUserUseCase
    {
        Task<dynamic> ExecuteAsync(UserEntity newUser);
    }
}
