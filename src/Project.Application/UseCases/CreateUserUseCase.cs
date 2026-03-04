using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class CreateUserUseCase : UseCaseBase, ICreateUserUseCase
    {
        private readonly IUserService _userService;

        public CreateUserUseCase(INotifier notifier, ILogger<CreateUserUseCase> logger, IUserService userService) : base(notifier, logger)
        {
            _userService = userService;
        }

        public async Task<dynamic> ExecuteAsync(UserEntity newUser)
        {
            var validationResult = await _userService.ValidateUserPasswordAsync(newUser.Password);
            if (!validationResult.IsValid)
            {
                return validationResult.Message;
            }
            var validatePhone = await _userService.ValidatePhoneNumberAsync(newUser.PhoneAttribute);
            if (!validatePhone.IsValid)
            {
                return validatePhone.Message;
            }

            var encryptedPassword = await _userService.EncryptPasswordAsync(newUser.Password);
            newUser.Password = encryptedPassword.HashedPassword;
            newUser.Salt = encryptedPassword.Salt;

            var email = await _userService.GetUserByEmailAsync(newUser.Email);
            if (email != null)
            {
                throw new ObjectValidationException("Email já utilizado");
            }

            var cpfCnpj = await _userService.GetUserByCpfCnpjAsync(newUser.CpfCnpj);
            if (cpfCnpj != null)
            {
                throw new ObjectValidationException("Cpf/Cnpj já utilizado");
            }

            await _userService.CreateUserAsync(newUser);

            return newUser;
        }

    }
}
