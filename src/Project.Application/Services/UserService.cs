using Project.Domain.Interfaces.Services;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Exceptions;
using Microsoft.Extensions.Configuration;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.Application.Base;

namespace Project.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly AsaasRequestBase _asaasRequestBase;

        public UserService(IUserRepository userRepository, AsaasRequestBase asaasRequestBase)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _asaasRequestBase = asaasRequestBase ?? throw new ArgumentNullException(nameof(asaasRequestBase));
        }

        public Task<dynamic> EncryptPasswordAsync(string password)
        {
            var salt = GenerateSalt();

            var hashedPassword = HashPassword(password, salt);

            return Task.FromResult<object>(new { HashedPassword = hashedPassword, Salt = salt });
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            var combinedPassword = password + salt;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public Task<bool> ValidatePasswordAsync(string password, string storedHash, string storedSalt)
        {
            var hashToValidate = ValidateHashPassword(password, storedSalt);

            var isPasswordValid = hashToValidate == storedHash;

            return Task.FromResult(isPasswordValid);
        }

        private string ValidateHashPassword(string password, string salt)
        {
            var combinedPassword = password + salt;

            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(combinedPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public Task<dynamic> ValidateUserPasswordAsync(string password)
        {
            if (password.Length < 6)
            {
                throw new ObjectValidationException("A senha deve ter pelo menos 6 caracteres");
            }

            if (!Regex.IsMatch(password, @"\d|\W"))
            {
                throw new ObjectValidationException("A senha deve conter pelo menos um número ou caractere especial");
            }

            return Task.FromResult<object>(new { IsValid = true, Message = "Senha válida" });
        }

        public Task<dynamic> ValidatePhoneNumberAsync(string phoneNumber)
        {

            if (string.IsNullOrEmpty(phoneNumber))
            {
                throw new ObjectValidationException("O número de telefone não pode estar vazio");
            }

            if (phoneNumber.Length != 11)
            {
                throw new ObjectValidationException("O número de telefone deve ter 11 dígitos");
            }

            return Task.FromResult<object>(new { IsValid = true, Message = "Número de telefone válido" });
        }

        public async Task CreateUserAsync(UserEntity newUser)
        {
            var (client, request) = _asaasRequestBase.CreateAsaasRequest("/customers", Method.Post);

            var requestBody = new
            {
                name = newUser.Name,
                cpfCnpj = newUser.CpfCnpj,
                email = newUser.Email,
                mobilePhone = newUser.PhoneAttribute,
                address = newUser.Address,
                addressNumber = newUser.AddressNumber,
                complement = newUser.Complement,
                province = newUser.Province,
                postalCode = newUser.PostalCode
            };

            request.AddJsonBody(requestBody);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonResponse = JObject.Parse(response.Content);
                string asaasUserId = jsonResponse["id"]?.ToString(); 
                newUser.Id = asaasUserId;

                await _userRepository.CreateAsync(newUser);
            }
            else
            {
                try
                {
                    var jsonResponse = JObject.Parse(response.Content);
                    var description = jsonResponse["errors"]?[0]?["description"]?.ToString();

                    if (!string.IsNullOrWhiteSpace(description))
                    {
                        throw new ObjectValidationException(description);
                    }
                    else
                    {
                        throw new ObjectValidationException("Erro desconhecido.");
                    }
                }
                catch (JsonReaderException)
                {
                    throw new ObjectValidationException("Erro ao processar resposta do servidor.");
                }
            }
        }

        public async Task<dynamic> GetUserByIdAsync(string id)
        {
            Object user = await _userRepository.GetUserByIdAsync(id);
            return user;
        }

        public async Task<dynamic> GetUserByEmailAsync(string email)
        {
            Object user = await _userRepository.GetUserByEmailAsync(email);
            return user;
        }

        public async Task<dynamic> GetUserByCpfCnpjAsync(string cpfCnpj)
        {
            Object user = await _userRepository.GetUserByCpfCnpjAsync(cpfCnpj);
            return user;
        }

    }
}
