using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entities
{
    public class UserEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string AddressNumber { get; set; }
        public string Complement { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneAttribute { get; set; }
        public string Salt { get; set; }

        public UserEntity() { }

        public UserEntity(string id, string name, string cpfCnpj, string postalCode, string address, string addressNumber, string complement, string province, string email, string password, string phoneAttribute)
        {
            Id = id;
            Name = name;
            CpfCnpj = cpfCnpj;
            PostalCode = postalCode;
            Address = address;
            AddressNumber = addressNumber;
            Complement = complement;
            Province = province;
            Email = email;
            Password = password;
            PhoneAttribute = phoneAttribute;
        }
    }
}
