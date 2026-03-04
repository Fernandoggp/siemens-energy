using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.Services
{
    public interface IGeneroService
    {
        Task<Result> ValidateNameAsync(string name);
        Task<Result> CreateGeneroAsync(GeneroEntity newGenero);
        Task<Result> GetAllGenerosAsync();
        Task<Result> UpdateGeneroAsync(GeneroEntity Genero);
        Task<Result> DeleteGeneroByIdAsync(Guid id);
    }
}
