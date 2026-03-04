using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Autor
{
    public interface IDeleteAutorByIdUseCase
    {
        Task<Result> ExecuteAsync(Guid id);
    }
}
