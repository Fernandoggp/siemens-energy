using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Project.Application.Base;
using Project.Domain.Entities;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Repositories;
using Project.Domain.Interfaces.Services;
using Project.Repository.Repositories;
using RestSharp;

namespace Project.Application.Services
{
    public class PaymentService: IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly AsaasRequestBase _asaasRequestBase;

        public PaymentService(IUserRepository userRepository, AsaasRequestBase asaasRequestBase, IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _asaasRequestBase = asaasRequestBase ?? throw new ArgumentNullException(nameof(asaasRequestBase));
        }

        public async Task<dynamic> CreateSignatureAsync(SignatureEntity newSignature)
        {
            var walletId = _configuration["Asaas:WalletId"];
            var (client, request) = _asaasRequestBase.CreateAsaasRequest("/creditCard/tokenizeCreditCard", Method.Post);

            var requestBody = new
            {
                customer = newSignature.ClientId,
                creditCard = new
                {
                    holderName = newSignature.CreditCard.HolderName,
                    number = newSignature.CreditCard.Number,
                    expiryMonth = newSignature.CreditCard.ExpiryMonth,
                    expiryYear = newSignature.CreditCard.ExpiryYear,
                    ccv = newSignature.CreditCard.CCV
                },
                creditCardHolder = new
                {
                    name = newSignature.CreditCardHolder.Name,
                    email = newSignature.CreditCardHolder.Email,
                    cpfCnpj = newSignature.CreditCardHolder.CpfCnpj,
                    postalCode = newSignature.CreditCardHolder.PostalCode,
                    addressNumber = newSignature.CreditCardHolder.AddressNumber,
                    addressComplement = newSignature.CreditCardHolder.AddressComplement,
                    phone = newSignature.CreditCardHolder.Phone
                },
                remoteIp = newSignature.Ip
            };

            request.AddJsonBody(requestBody);
            var tokenResponse = await client.ExecuteAsync(request);

            if (tokenResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                HandleErrorResponse(tokenResponse);
            }

            var tokenJson = JObject.Parse(tokenResponse.Content);
            var creditCardToken = tokenJson["creditCardToken"]?.ToString();

            if (string.IsNullOrWhiteSpace(creditCardToken))
            {
                throw new ObjectValidationException("Erro ao obter token do cartão.");
            }

            var (subscriptionClient, subscriptionRequest) = _asaasRequestBase.CreateAsaasRequest("/subscriptions", Method.Post);

            var subscriptionRequestBody = new
            {
                customer = newSignature.ClientId,
                billingType = newSignature.Type.ToString(),
                nextDueDate = DateTime.UtcNow.ToString("yyyy-MM-dd"),
                value = CalculatePlanValue(newSignature.Plan, newSignature.Installments),
                cycle = "MONTHLY",
                description = "Assinatura Capital Control",
                maxPayments = newSignature.Installments,
                split = new[] { new { walletId = walletId } },
                callback = new { successUrl = "UrlBase" },
                creditCardToken = creditCardToken,
                remoteIp = newSignature.Ip
            };

            subscriptionRequest.AddJsonBody(subscriptionRequestBody);
            var subscriptionResponse = await subscriptionClient.ExecuteAsync(subscriptionRequest);

            if (subscriptionResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                HandleErrorResponse(subscriptionResponse);
            }

            return subscriptionResponse;
        }

        private void HandleErrorResponse(RestResponse response)
        {
            try
            {
                var jsonResponse = JObject.Parse(response.Content);
                var description = jsonResponse["errors"]?[0]?["description"]?.ToString();

                throw new ObjectValidationException(!string.IsNullOrWhiteSpace(description) ? description : "Erro desconhecido.");
            }
            catch (JsonReaderException)
            {
                throw new ObjectValidationException("Erro ao processar resposta do servidor.");
            }
        }

        private decimal CalculatePlanValue(PlanType plan, int installments)
        {
            decimal monthlyValue = plan switch
            {
                PlanType.QUARTERLY => 59.90m,
                PlanType.SEMIANNUALLY => 49.90m,
                PlanType.YEARLY => 27.90m,
                _ => throw new ObjectValidationException("Plano inválido")
            };
            decimal totalValue = monthlyValue * (plan == PlanType.QUARTERLY ? 3 : plan == PlanType.SEMIANNUALLY ? 6 : 12);
            return totalValue / installments;
        }

        public Task<dynamic> ValidatePaymentTypeAsync(string type, string plan, int installments, object creditCard, object creditCreditHolder)
        {
            if (!Enum.TryParse(type, out PaymentType paymentType))
            {
                throw new ObjectValidationException("Tipo de pagamento inválido");
            }

            if (!Enum.TryParse(plan, out PlanType planType))
            {
                throw new ObjectValidationException("Plano inválido");
            }

            if (paymentType == PaymentType.PIX)
            {
                if (installments > 1)
                {
                    throw new ObjectValidationException("Pagamentos via PIX devem ser feitos em uma única parcela");
                }

                if (creditCard != null)
                {
                    throw new ObjectValidationException("Pagamentos via PIX não devem conter informações de cartão de crédito");
                }
            }
            else if (paymentType == PaymentType.CREDIT_CARD)
            {
                if (creditCard == null || creditCreditHolder == null)
                {
                    throw new ObjectValidationException("Pagamentos com cartão de crédito exigem informações do cartão e do titular");
                }

                int maxInstallments = planType switch
                {
                    PlanType.QUARTERLY => 3,
                    PlanType.SEMIANNUALLY => 6,
                    PlanType.YEARLY => 12,
                    _ => throw new ObjectValidationException("Plano desconhecido")
                };

                if (installments > maxInstallments)
                {
                    throw new ObjectValidationException($"O número máximo de parcelas para o plano {planType} é {maxInstallments}");
                }
            }
            else
            {
                throw new ObjectValidationException("Tipo de pagamento não suportado");
            }

            return Task.FromResult<object>(new { IsValid = true, Message = "Pagamento válido" });
        }
    }
}
