using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Livro
{
    public class CreateLivroDto
    {
        [Required] public string Name { get; set; }
        [Required] public Guid AutorId { get; set; }
        [Required] public Guid GeneroId { get; set; }
    }
}
