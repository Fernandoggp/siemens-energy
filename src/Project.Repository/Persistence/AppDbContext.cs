using Microsoft.EntityFrameworkCore;
using Project.Domain.Entities;

namespace Project.Repository.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<AutorEntity> Autores { get; set; }
    }
}