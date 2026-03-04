using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface ILivroService
    {
        Task<Result> ValidateNameAsync(string name);
        Task<Result> CreateLivroAsync(LivroEntity newLivro);
        Task<Result> GetAllLivrosAsync();
        Task<Result> GetFilteredLivrosAsync(Guid? livroId, Guid? autorId, Guid? generoId);
        Task<Result> UpdateLivroAsync(LivroEntity livro);
        Task<Result> DeleteLivroByIdAsync(Guid id);
    }
}
