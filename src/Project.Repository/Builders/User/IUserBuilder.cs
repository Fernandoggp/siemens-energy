using Project.Domain.Entities;
using Project.Repository.Core;

namespace Project.Repository.Builders.User
{
    public interface IUserBuilder
    {
        Request CreateUser(UserEntity newUser);
        Request GetUserById(string id);
        Request GetUserByEmail(string email);
        Request GetUserByCpfCnpj(string cpfCnpj);
    }
}
