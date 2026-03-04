using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases.Autor
{
    public interface IUpdateAutorUseCase
    {
        Task<Result> ExecuteAsync(AutorEntity autor);
    }
}
