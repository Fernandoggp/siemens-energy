using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class CreateSignatureUseCase : UseCaseBase, ICreateSignatureUseCase
    {
        private readonly IUserService _userService;
        private readonly IPaymentService _paymentService;

        public CreateSignatureUseCase(INotifier notifier, ILogger<CreateSignatureUseCase> logger, IPaymentService paymentService, IUserService userService) : base(notifier, logger)
        {
            _paymentService = paymentService;
            _userService = userService;
        }

        public async Task<dynamic> ExecuteAsync(SignatureEntity newSignature)
        {
            var client = await _userService.GetUserByIdAsync(newSignature.ClientId);
            if (client == null)
            {
                throw new ObjectValidationException("Cliente não encontrado");
            }
            var validatePayment = await _paymentService.ValidatePaymentTypeAsync(newSignature.Type.ToString(), newSignature.Plan.ToString(), newSignature.Installments, newSignature.CreditCard, newSignature.CreditCardHolder);
            if (!validatePayment.IsValid)
            {
                return validatePayment.Message;
            }

            await _paymentService.CreateSignatureAsync(newSignature);

            return newSignature;
        }
    }
}
