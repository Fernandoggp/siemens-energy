using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Autor
{
    public interface IGetAllAutoresUseCase
    {
        Task<Result> ExecuteAsync();
    }
}
