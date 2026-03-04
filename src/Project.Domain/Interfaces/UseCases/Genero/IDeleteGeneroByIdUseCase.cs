using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Genero
{
    public interface IDeleteGeneroByIdUseCase
    {
        Task<Result> ExecuteAsync(Guid id);
    }
}
