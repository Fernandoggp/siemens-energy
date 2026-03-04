using Project.Domain.Entities;
using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Genero
{
    public interface ICreateGeneroUseCase
    {
        Task<Result> ExecuteAsync(GeneroEntity genero);
    }
}
