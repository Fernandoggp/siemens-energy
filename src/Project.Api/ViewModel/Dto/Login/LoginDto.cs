using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Login
{
    public class LoginDto
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
