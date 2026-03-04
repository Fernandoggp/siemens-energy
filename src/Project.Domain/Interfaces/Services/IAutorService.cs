using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface IAutorService
    {
        Task<Result> ValidateNameAsync(string name);
        Task<Result> CreateAutorAsync(AutorEntity newAutor);
        Task<Result> GetAllAutoresAsync();
        Task<Result> UpdateAutorAsync(AutorEntity Autor);
        Task<Result> DeleteAutorByIdAsync(Guid id);
    }
}
