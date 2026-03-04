using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Livro
{
    public interface IGetAllLivrosUseCase
    {
        Task<Result> ExecuteAsync();
    }
}
