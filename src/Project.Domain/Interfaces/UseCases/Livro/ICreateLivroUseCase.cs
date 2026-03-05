using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases.Livro
{
    public interface ICreateLivroUseCase
    {
        Task<Result> ExecuteAsync(LivroEntity livro);
    }
}
