using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Livro
{
    public interface IDeleteLivroByIdUseCase
    {
        Task<Result> ExecuteAsync(Guid id);
    }
}
