using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Genero
{
    public class UpdateGeneroDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
