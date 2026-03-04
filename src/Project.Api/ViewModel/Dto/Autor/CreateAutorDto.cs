using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Autor
{
    public class CreateAutorDto
    {
        [Required] public string Name { get; set; }
    }
}
