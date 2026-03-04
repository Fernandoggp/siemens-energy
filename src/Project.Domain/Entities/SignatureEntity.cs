using System.ComponentModel.DataAnnotations;

namespace Project.Domain.Entities
{
    public class SignatureEntity
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        public PlanType Plan { get; set; }

        [Required]
        public int Installments { get; set; }

        public CreditCard CreditCard { get; set; }

        public CreditCardHolder CreditCardHolder { get; set; }

        [Required]
        public string Ip {  get; set; }
    }

    public enum PaymentType
    {
        CREDIT_CARD,
        PIX
    }

    public enum PlanType
    {
        QUARTERLY,
        SEMIANNUALLY,
        YEARLY
    }

    public class CreditCard
    {
        [Required]
        public string HolderName { get; set; }

        [Required]
        public string Number { get; set; }

        [Required]
        public string ExpiryMonth { get; set; }

        [Required]
        public string ExpiryYear { get; set; }

        [Required]
        public string CCV { get; set; }
    }

    public class CreditCardHolder
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string CpfCnpj { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
