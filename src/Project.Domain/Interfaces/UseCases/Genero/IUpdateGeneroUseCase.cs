using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases.Genero
{
    public interface IUpdateGeneroUseCase
    {
        Task<Result> ExecuteAsync(GeneroEntity genero);
    }
}
