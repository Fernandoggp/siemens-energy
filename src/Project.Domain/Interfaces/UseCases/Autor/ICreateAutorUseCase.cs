using Project.Domain.Entities;
using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Autor
{
    public interface ICreateAutorUseCase
    {
        Task<Result> ExecuteAsync(AutorEntity autor);
    }
}
