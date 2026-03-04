using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class LoginUseCase : UseCaseBase, ILoginUseCase
    {
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;

        public LoginUseCase(INotifier notifier, ILogger<LoginUseCase> logger, ILoginService loginService, IUserService userService) : base(notifier, logger)
        {
            _loginService = loginService;
            _userService = userService;
        }

        public async Task<dynamic> ExecuteAsync(LoginEntity login)
        {
            var user = await _userService.GetUserByEmailAsync(login.Email);
            if (user == null)
            {
                throw new ObjectValidationException("Email ou senha inválido");
            }

            bool password = await _loginService.ValidatePasswordAsync(login.Password, user.password, user.salt);
            if(!password)
            {
                throw new ObjectValidationException("Email ou senha inválido");
            }

            string token = await _loginService.GenerateJwtToken(user.email, user.id);

            return new
            {
                Success = true,
                User = user.id,
                Token = token,
                Message = "Login realizado com sucesso"
            };
        }
    }
}
