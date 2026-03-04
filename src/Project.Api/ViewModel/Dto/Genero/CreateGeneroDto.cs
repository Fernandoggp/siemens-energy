using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Genero
{
    public class CreateGeneroDto
    {
        [Required] public string Name { get; set; }
    }
}
