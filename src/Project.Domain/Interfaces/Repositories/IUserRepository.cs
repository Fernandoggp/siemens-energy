using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<dynamic> CreateAsync(UserEntity newUser);
        Task<dynamic> GetUserByIdAsync(string id);
        Task<dynamic> GetUserByEmailAsync(string email);
        Task<dynamic> GetUserByCpfCnpjAsync(string cpfCnpj);
    }
}
