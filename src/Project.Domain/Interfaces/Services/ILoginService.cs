namespace Project.Domain.Interfaces.Services
{
    public interface ILoginService
    {
        Task<bool> ValidatePasswordAsync(string password, string storedHash, string storedSalt);
        Task<string> GenerateJwtToken(string email, string id);
    }
}
