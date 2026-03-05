using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.Services;

namespace Project.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository
                ?? throw new ArgumentNullException(nameof(autorRepository));
        }

        public async Task<Result> ValidateNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Nome é obrigatório");

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                return Result.Fail("Nome só deve conter letras");

            if (name.Length < 3 || name.Length > 200)
                return Result.Fail("Nome deve ter entre 3 e 200 caracteres");

            var exists = await _autorRepository.ExistsByNameAsync(name);

            if (exists)
                return Result.Fail("Já existe um autor com esse nome.");

            return Result.Ok();
        }

        public async Task<Result> CreateAutorAsync(AutorEntity newAutor)
        {
            if (newAutor is null)
                return Result.Fail("Autor inválido.");

            await _autorRepository.CreateAsync(newAutor);

            return Result.Ok(newAutor, System.Net.HttpStatusCode.Created);
        }

        public async Task<Result> GetAllAutoresAsync()
        {
            var autores = await _autorRepository.GetAllAutoresAsync();

            return Result.Ok(autores);
        }

        public async Task<Result> UpdateAutorAsync(AutorEntity autor)
        {
            var updatedAutor = await _autorRepository.UpdateAsync(autor);

            if (updatedAutor is null)
                return Result.Fail("Autor não encontrado na base de dados para atualização.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(updatedAutor);
        }

        public async Task<Result> DeleteAutorByIdAsync(Guid id)
        {
            var autor = await _autorRepository.DeleteAutorByIdAsync(id);

            if (autor is false)
                return Result.Fail("Autor não encontrado na base de dados para deleção.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(autor);
        }

        public async Task<Result> GetAutorByIdAsync(Guid id)
        {
            var autor = await _autorRepository.GetAutorByIdAsync(id);

            if (autor is null)
                return Result.Fail("Autor não encontrado na base de dados.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(autor);
        }
    }
}