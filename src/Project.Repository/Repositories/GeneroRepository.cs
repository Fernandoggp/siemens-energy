using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;
using Project.Domain.Interfaces.Repositories;
using Project.Repository.Persistence;

namespace Project.Infrastructure.Persistence.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _context.Generos
                .AnyAsync(a => a.Name == name);
        }

        public async Task<GeneroEntity> CreateAsync(GeneroEntity newGenero)
        {
            await _context.Generos.AddAsync(newGenero);
            await _context.SaveChangesAsync();

            return newGenero;
        }

        public async Task<IEnumerable<GeneroEntity>> GetAllGenerosAsync()
        {
            return await _context.Generos
                .AsNoTracking()
                .OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<GeneroEntity> UpdateAsync(GeneroEntity genero)
        {
            var exists = await _context.Generos.AnyAsync(x => x.Id == genero.Id);

            if (!exists)
                return null;

            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();

            return genero;
        }

        public async Task<bool> DeleteGeneroByIdAsync(Guid id)
        {
            var genero = await _context.Generos.FirstOrDefaultAsync(x => x.Id == id);

            if (genero == null)
                return false;

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<GeneroEntity> GetGeneroByIdAsync(Guid id)
        {
            var genero = await _context.Generos
                .FirstOrDefaultAsync(x => x.Id == id);

            return genero;
        }

    }
}