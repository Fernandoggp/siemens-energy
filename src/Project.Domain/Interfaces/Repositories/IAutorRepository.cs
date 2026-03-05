using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Repositories
{
    public interface IAutorRepository
    {
        Task<AutorEntity> CreateAsync(AutorEntity newAutor);
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<AutorEntity>> GetAllAutoresAsync();
        Task<AutorEntity> UpdateAsync(AutorEntity Autor);
        Task<bool> DeleteAutorByIdAsync(Guid id);
        Task<AutorEntity> GetAutorByIdAsync(Guid id);
    }
}
