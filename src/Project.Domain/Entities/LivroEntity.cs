namespace Project.Domain.Entities
{
    public class LivroEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid AutorId { get; set; }
        public AutorEntity Autor { get; set; }

        public Guid GeneroId { get; set; }
        public GeneroEntity Genero { get; set; }
    }
}
