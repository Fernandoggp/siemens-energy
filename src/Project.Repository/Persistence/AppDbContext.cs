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
        public DbSet<GeneroEntity> Generos { get; set; }
        public DbSet<LivroEntity> Livros { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Livro - Autor
            modelBuilder.Entity<LivroEntity>()
                .HasOne(l => l.Autor)
                .WithMany() 
                .HasForeignKey(l => l.AutorId)
                .OnDelete(DeleteBehavior.Restrict); 

            // Livro - Genero
            modelBuilder.Entity<LivroEntity>()
                .HasOne(l => l.Genero)
                .WithMany() 
                .HasForeignKey(l => l.GeneroId)
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}