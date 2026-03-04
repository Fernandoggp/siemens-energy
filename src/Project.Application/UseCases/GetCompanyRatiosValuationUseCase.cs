using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class GetCompanyRatiosValuationUseCase: UseCaseBase, IGetCompanyRatiosValuationUseCase
    {
        private readonly ICedroService _cedroService;

        public GetCompanyRatiosValuationUseCase(INotifier notifier, ILogger<GetCompanyRatiosValuationUseCase> logger, ICedroService cedroService) : base(notifier, logger)
        {
            _cedroService = cedroService;
        }

        public async Task<dynamic> ExecuteAsync(string ticker)
        {
            var company = await _cedroService.GetCompanyRatiosValuationAsync(ticker);
            return company;
        }
    }
}
