using Project.Domain.Common;
using Project.Domain.Entities;

namespace Project.Domain.Interfaces.UseCases.Livro
{
    public interface IUpdateLivroUseCase
    {
        Task<Result> ExecuteAsync(LivroEntity livro);
    }
}
