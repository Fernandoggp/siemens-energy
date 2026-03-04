using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.Services;

namespace Project.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository
                ?? throw new ArgumentNullException(nameof(generoRepository));
        }

        public async Task<Result> ValidateNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Nome é obrigatório");

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                return Result.Fail("Nome só deve conter letras");

            if (name.Length < 3 || name.Length > 200)
                return Result.Fail("Nome deve ter entre 3 e 200 caracteres");

            var exists = await _generoRepository.ExistsByNameAsync(name);

            if (exists)
                return Result.Fail("Já existe um genero com esse nome.");

            return Result.Ok();
        }

        public async Task<Result> CreateGeneroAsync(GeneroEntity newGenero)
        {
            if (newGenero is null)
                return Result.Fail("Genero inválido.");

            await _generoRepository.CreateAsync(newGenero);

            return Result.Ok(newGenero, System.Net.HttpStatusCode.Created);
        }

        public async Task<Result> GetAllGenerosAsync()
        {
            var generos = await _generoRepository.GetAllGenerosAsync();

            return Result.Ok(generos);
        }

        public async Task<Result> UpdateGeneroAsync(GeneroEntity genero)
        {
            var updatedGenero = await _generoRepository.UpdateAsync(genero);

            if (updatedGenero is null)
                return Result.Fail("Genero não encontrado na base de dados para atualização.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(updatedGenero);
        }

        public async Task<Result> DeleteGeneroByIdAsync(Guid id)
        {
            var genero = await _generoRepository.DeleteGeneroByIdAsync(id);

            if (genero is false)
                return Result.Fail("Genero não encontrado na base de dados para atualização.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(genero);
        }
    }
}