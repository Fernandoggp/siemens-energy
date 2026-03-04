using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Payment
{
    public class CreateSignatureDto
    {
        [Required]
        public string clientId { get; set; }
        [Required]
        public PaymentTypeEnum type { get; set; }
        [Required]
        public PlanEnum plan { get; set; }
        [Required]
        public int installments { get; set; }

        public CreditCardInfo creditCard { get; set; }

        public CreditCardHolderInfo creditCardHolder { get; set; }

        [Required]
        public string Ip { get; set; }
    }

    public enum PaymentTypeEnum
    {
        CREDIT_CARD,
        PIX
    }

    public enum PlanEnum
    {
        QUARTERLY,
        SEMIANNUALLY,
        YEARLY
    }

    public class CreditCardInfo
    {
        [Required]
        public string holderName { get; set; }
        [Required]
        public string number { get; set; }
        [Required]
        public string expiryMonth { get; set; }
        [Required]
        public string expiryYear { get; set; }
        [Required]
        public string ccv { get; set; }
    }

    public class CreditCardHolderInfo
    {
        [Required]
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string cpfCnpj { get; set; }
        [Required]
        public string postalCode { get; set; }
        [Required]
        public string addressNumber { get; set; }
        public string addressComplement { get; set; }
        [Required]
        public string phone { get; set; }
    }
}
