using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<dynamic> ValidateUserPasswordAsync(string password);
        Task<dynamic> EncryptPasswordAsync(string password);
        Task<bool> ValidatePasswordAsync(string password, string storedHash, string storedSalt);
        Task CreateUserAsync(UserEntity newUser);
        Task<dynamic> ValidatePhoneNumberAsync(string phoneNumber);
        Task<dynamic> GetUserByIdAsync(string id);
        Task<dynamic> GetUserByEmailAsync(string email);
        Task<dynamic> GetUserByCpfCnpjAsync(string cpfCnpj);
    }
}
