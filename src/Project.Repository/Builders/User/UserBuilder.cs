using Project.Domain.Entities;
using Project.Repository.Core;

namespace Project.Repository.Builders.User
{
    public partial class UserBuilder : IUserBuilder
    {
        public Request CreateUser(UserEntity newUser)
        {
            Id = newUser.Id;
            CpfCnpj = newUser.CpfCnpj;
            Email = newUser.Email;
            Password = newUser.Password;
            Salt = newUser.Salt;

            var sql = CreateUserSql();

            return new Request(sql, null);
        }

        public Request GetUserById(string id)
        {
            Id = id;
            var sql = GetUserByIdSql();

            return new Request(sql, null);
        }

        public Request GetUserByEmail(string email)
        {
            Email = email;
            var sql = GetUserByEmailSql();

            return new Request(sql, null);
        }

        public Request GetUserByCpfCnpj(string cpfCnpj)
        {
            CpfCnpj = cpfCnpj;
            var sql = GetUserByCpfCnpjSql();

            return new Request(sql, null);
        }

    }
}

