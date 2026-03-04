using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Application.Services;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class GetCompanyUseCase : UseCaseBase, IGetCompanyUseCase
    {
        private readonly ICedroService _cedroService;

        public GetCompanyUseCase(INotifier notifier, ILogger<GetCompanyUseCase> logger, ICedroService cedroService) : base(notifier, logger)
        {
            _cedroService = cedroService;
        }
    
        public async Task<dynamic> ExecuteAsync(string ticker)
        {
            var company = await _cedroService.GetCompanyAsync(ticker);
            return company;
        }
    }
}
