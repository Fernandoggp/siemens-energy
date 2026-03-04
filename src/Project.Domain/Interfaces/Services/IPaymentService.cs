using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<dynamic> CreateSignatureAsync(SignatureEntity newSignature);
        Task<dynamic> ValidatePaymentTypeAsync(string type, string plan, int installments, object creditCard, object creditCardHolder);
    }
}
