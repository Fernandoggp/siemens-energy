using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Livro
{
    public interface IGetFilteredLivrosUseCase
    {
        Task<Result> ExecuteAsync(Guid? livroId, Guid? autorId, Guid? generoId);
    }
}
