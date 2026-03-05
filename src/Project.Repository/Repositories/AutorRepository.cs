using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Persistence;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly AppDbContext _context;

        public AutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Autores
                .AnyAsync(a => a.Name == name);
        }

        public async Task<AutorEntity> CreateAsync(AutorEntity newAutor)
        {
            await _context.Autores.AddAsync(newAutor);
            await _context.SaveChangesAsync();

            return newAutor;
        }

        public async Task<IEnumerable<AutorEntity>> GetAllAutoresAsync()
        {
            return await _context.Autores
                .AsNoTracking() 
                .OrderBy(a => a.Name) 
                .ToListAsync();
        }

        public async Task<AutorEntity> UpdateAsync(AutorEntity autor)
        {
            var exists = await _context.Autores.AnyAsync(x => x.Id == autor.Id);

            if (!exists)
                return null;

            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();

            return autor;
        }

        public async Task<bool> DeleteAutorByIdAsync(Guid id)
        {
            var autor = await _context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
                return false;

            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<AutorEntity> GetAutorByIdAsync(Guid id)
        {
            var autor = await _context.Autores
                .FirstOrDefaultAsync(x => x.Id == id);

            return autor;
        }

    }
}