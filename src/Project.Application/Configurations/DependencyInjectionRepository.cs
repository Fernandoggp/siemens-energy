using Deviot.Common;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Services.Http;
using Project.Domain.Interfaces.Http;
using Project.Application.UseCases;
using Project.Domain.Interfaces.UseCases;
using Project.Domain.Interfaces.Services;
using Project.Application.Services;
using Microsoft.Extensions.Configuration;
using Project.Application.Base;

namespace Project.Application.Configurations
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddDependencyInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // HTTP
            services.AddTransient<IHttpClientService, HttpClientService>();

            // Common
            services.AddSingleton<INotifier, Notifier>();

            // Use Cases
            services.AddSingleton<IStockPortfolioReportUseCase, StockPortfolioReportUseCase>();
            services.AddSingleton<IStockReportUseCase, StockReportUseCase>();
            services.AddSingleton<IFixedIncomeCalculatorUseCase, FixedIncomeCalculatorUseCase>();
            services.AddSingleton<ICompoundInterestCalculatorUseCase, CompoundInterestCalculatorUseCase>();
            services.AddSingleton<ICreateUserUseCase, CreateUserUseCase>();
            services.AddSingleton<ILoginUseCase, LoginUseCase>();
            services.AddSingleton<IStockNewInvestmentUseCase, StockNewInvestmentUseCase>();
            services.AddSingleton<ICreateSignatureUseCase, CreateSignatureUseCase>();
            services.AddSingleton<ICreateAssetUseCase, CreateAssetUseCase>();
            services.AddSingleton<IUpdateAssetUseCase, UpdateAssetUseCase>();
            services.AddSingleton<IGetAssetsUseCase, GetAssetsUseCase>();
            services.AddSingleton<IDeleteAssetUseCase, DeleteAssetUseCase>();
            services.AddSingleton<IListCompaniesUseCase, ListCompaniesUseCase>();
            services.AddSingleton<IGetCompanyUseCase, GetCompanyUseCase>();
            services.AddSingleton<IGetCompanyRatiosUseCase, GetCompanyRatiosUseCase>();
            services.AddSingleton<IGetCompanyRatiosValuationUseCase, GetCompanyRatiosValuationUseCase>();
            services.AddSingleton<IGetCompanyRawReportsUseCase, GetCompanyRawReportsUseCase>();

            // Services
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<ILoginService, LoginService>();
            services.AddSingleton<IStockService, StockService>();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddSingleton<IPortfolioService, PortfolioService>();
            services.AddSingleton<ICedroService, CedroService>();

            // Asaas
            services.AddSingleton<AsaasRequestBase>();
            
            // Cedro
            services.AddSingleton<CedroRequestBase>();

            return services;
        }
    }
}
