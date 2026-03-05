using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        Task<LivroEntity> CreateAsync(LivroEntity newLivro);
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<LivroEntity>> GetAllLivrosAsync();
        Task<IEnumerable<LivroEntity>> GetFilteredLivrosAsync(Guid? livroId, Guid? autorId, Guid? generoId);
        Task<LivroEntity> UpdateAsync(LivroEntity Livro);
        Task<bool> DeleteLivroByIdAsync(Guid id);
    }
}
