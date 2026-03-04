using Project.Api.ViewModel.Dto.User;
using Project.Api.ViewModel.Dto.Portfolio;
using Project.Application.Dtos;
using Project.Domain.Entities;
using Project.Api.ViewModel.Dto.Login;
using Project.Api.ViewModel.Dto.Stock;
using Project.Api.ViewModel.Dto.Payment;

namespace Project.Api.Mappings
{
    public class ViewModelToEntityMapping
    {
        public static List<SimpleStockEntity> MapStockPortfolioReportToEntity(StockPortfolioReportDto dto)
        {
            return dto.Stocks.Select(stock => new SimpleStockEntity(
                stock.Code,
                stock.Value
            )).ToList();
        }

        public static List<FixedIncomeCalculatorEntity> MapFixedIncomeCalculatorToEntity(FixedIncomeCalculatorDto dto)
        {
            return dto.asset.Select(asset => new FixedIncomeCalculatorEntity(
                asset.type.ToString(),
                asset.modality.ToString(),
                asset.profitability,
                asset.months
            )).ToList();
        }

        public static UserEntity MapUserDtoToEntity(CreateUserDto dto)
        {
            return new UserEntity
            {
                Name = dto.Name,
                CpfCnpj = dto.CpfCnpj,
                PostalCode = dto.PostalCode,
                Address = dto.Address,
                AddressNumber = dto.AddressNumber,
                Complement = dto.Complement,
                Province = dto.Province,
                Email = dto.Email,
                Password = dto.Password,
                PhoneAttribute = dto.PhoneAttribute
            };
        }

        public static LoginEntity MapLoginDtoToEntity(LoginDto dto)
        {
            return new LoginEntity
            {
                Email = dto.email,
                Password = dto.password
            };
        }

        public static List<StockNewInvestmentEntity> MapStockNewInvestmentDtoToEntity(StockNewInvestmentDto dto)
        {
            return dto.Stocks.Select(stock => new StockNewInvestmentEntity(
                stock.Name,
                stock.Value,
                stock.DesiredPercentage
            )).ToList();
        }

        public static SignatureEntity MapSignatureDtoToEntity(CreateSignatureDto dto)
        {
            return new SignatureEntity
            {
                ClientId = dto.clientId,
                Type = (PaymentType)dto.type,
                Plan = (PlanType)dto.plan,
                Installments = dto.installments,
                CreditCard = dto.creditCard != null ? new CreditCard
                {
                    HolderName = dto.creditCard.holderName,
                    Number = dto.creditCard.number,
                    ExpiryMonth = dto.creditCard.expiryMonth,
                    ExpiryYear = dto.creditCard.expiryYear,
                    CCV = dto.creditCard.ccv
                } : null,
                CreditCardHolder = dto.creditCardHolder != null ? new CreditCardHolder
                {
                    Name = dto.creditCardHolder.name,
                    Email = dto.creditCardHolder.email,
                    CpfCnpj = dto.creditCardHolder.cpfCnpj,
                    PostalCode = dto.creditCardHolder.postalCode,
                    AddressNumber = dto.creditCardHolder.addressNumber,
                    AddressComplement = dto.creditCardHolder.addressComplement,
                    Phone = dto.creditCardHolder.phone
                } : null,
                Ip = dto.Ip
            };
        }

        public static AssetEntity MapCreateAssetDtoToEntity(CreateAssetDto dto)
        {
            return new AssetEntity
            {
                Id = Guid.NewGuid(),
                Name = dto.name,
                Value = dto.value,
                DesiredPercentage = dto.desiredPercentage,
                UserId = dto.userId
            };
        }

        public static AssetEntity MapUpdateAssetDtoToEntity(UpdateAssetDto dto)
        {
            return new AssetEntity
            {
                Id = dto.id,
                Name = dto.name,
                Value = dto.value,
                DesiredPercentage = dto.desiredPercentage,
                UserId = dto.user_id
            };
        }

    }
}
