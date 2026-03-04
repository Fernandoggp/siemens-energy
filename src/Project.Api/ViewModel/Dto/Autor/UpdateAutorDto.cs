using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Autor
{
    public class UpdateAutorDto
    {
        [Required] public Guid Id { get; set; }
        [Required] public string Name { get; set; }
    }
}
