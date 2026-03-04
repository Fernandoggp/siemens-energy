using Project.Domain.Common;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Services
{
    public class LivroService: ILivroService
    {
        private readonly ILivroRepository _livroRepository;

        public LivroService(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository
                ?? throw new ArgumentNullException(nameof(livroRepository));
        }

        public async Task<Result> ValidateNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Result.Fail("Nome é obrigatório");

            if (!name.All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                return Result.Fail("Nome só deve conter letras");

            if (name.Length < 3 || name.Length > 200)
                return Result.Fail("Nome deve ter entre 3 e 200 caracteres");

            var exists = await _livroRepository.ExistsByNameAsync(name);

            if (exists)
                return Result.Fail("Já existe um livro com esse nome.");

            return Result.Ok();
        }

        public async Task<Result> CreateLivroAsync(LivroEntity newLivro)
        {
            if (newLivro is null)
                return Result.Fail("Livro inválido.");

            await _livroRepository.CreateAsync(newLivro);

            return Result.Ok(newLivro, System.Net.HttpStatusCode.Created);
        }

        public async Task<Result> GetAllLivrosAsync()
        {
            var livros = await _livroRepository.GetAllLivrosAsync();

            return Result.Ok(livros);
        }

        public async Task<Result> GetFilteredLivrosAsync(Guid? livroId, Guid? autorId, Guid? generoId)
        {
            var livros = await _livroRepository.GetFilteredLivrosAsync(livroId, autorId, generoId);

            return Result.Ok(livros);
        }

        public async Task<Result> UpdateLivroAsync(LivroEntity livro)
        {
            var updatedLivro = await _livroRepository.UpdateAsync(livro);

            if (updatedLivro is null)
                return Result.Fail("Livro não encontrado na base de dados para atualização.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(updatedLivro);
        }

        public async Task<Result> DeleteLivroByIdAsync(Guid id)
        {
            var livro = await _livroRepository.DeleteLivroByIdAsync(id);

            if (livro is false)
                return Result.Fail("Livro não encontrado na base de dados para deleção.", System.Net.HttpStatusCode.NotFound);

            return Result.Ok(livro);
        }
    }
}
