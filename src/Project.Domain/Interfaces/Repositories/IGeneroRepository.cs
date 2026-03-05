using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Repositories
{
    public interface IGeneroRepository
    {
        Task<GeneroEntity> CreateAsync(GeneroEntity newGenero);
        Task<bool> ExistsByNameAsync(string name);
        Task<IEnumerable<GeneroEntity>> GetAllGenerosAsync();
        Task<GeneroEntity> UpdateAsync(GeneroEntity Genero);
        Task<bool> DeleteGeneroByIdAsync(Guid id);
        Task<GeneroEntity> GetGeneroByIdAsync(Guid id);
    }
}
