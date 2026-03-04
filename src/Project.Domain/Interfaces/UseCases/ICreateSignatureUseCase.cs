using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases
{
    public interface ICreateSignatureUseCase
    {
        Task<dynamic> ExecuteAsync(SignatureEntity newSignature);
    }
}
