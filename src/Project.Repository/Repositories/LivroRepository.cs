using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Persistence;

namespace Project.Repository.Repositories
{
    public class LivroRepository: ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid? id = null)
        {
            return await _context.Livros
                .AnyAsync(a => EF.Functions.ILike(a.Name, name) &&
                               (!id.HasValue || a.Id != id.Value));
        }

        public async Task<LivroEntity> CreateAsync(LivroEntity newLivro)
        {
            await _context.Livros.AddAsync(newLivro);
            await _context.SaveChangesAsync();

            return newLivro;
        }

        public async Task<IEnumerable<LivroEntity>> GetAllLivrosAsync()
        {
            return await _context.Livros
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<LivroEntity>> GetFilteredLivrosAsync(Guid? livroId, Guid? autorId, Guid? generoId)
        {
            var query = _context.Livros.AsQueryable();

            if (livroId.HasValue)
                query = query.Where(l => l.Id == livroId.Value);

            if (autorId.HasValue)
                query = query.Where(l => l.AutorId == autorId.Value);

            if (generoId.HasValue)
                query = query.Where(l => l.GeneroId == generoId.Value);

            return await query.ToListAsync();
        }

        public async Task<LivroEntity> UpdateAsync(LivroEntity livro)
        {
            var exists = await _context.Livros.AnyAsync(x => x.Id == livro.Id);

            if (!exists)
                return null;

            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();

            return livro;
        }

        public async Task<bool> DeleteLivroByIdAsync(Guid id)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(x => x.Id == id);

            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
