using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.User
{
    public class CreateUserDto
    {
        [Required] public string Name { get; set; }
        [Required] public string CpfCnpj { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string Complement { get; set; }
        public string Province { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string PhoneAttribute { get; set; }
    }
}
