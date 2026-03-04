using Project.Domain.Common;

namespace Project.Domain.Interfaces.UseCases.Genero
{
    public interface IGetAllGenerosUseCase
    {
        Task<Result> ExecuteAsync();
    }
}
