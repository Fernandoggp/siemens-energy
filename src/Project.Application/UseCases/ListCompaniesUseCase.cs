using Deviot.Common;
using Microsoft.Extensions.Logging;
using Project.Application.Base;
using Project.Domain.Interfaces.Services;
using Project.Domain.Interfaces.UseCases;

namespace Project.Application.UseCases
{
    public class ListCompaniesUseCase : UseCaseBase, IListCompaniesUseCase
    {
        private readonly ICedroService _cedroService;

        public ListCompaniesUseCase(INotifier notifier, ILogger<ListCompaniesUseCase> logger, ICedroService cedroService) : base(notifier, logger)
        {
            _cedroService = cedroService;
        }

        public async Task<dynamic> ExecuteAsync()
        {
            var companies = await _cedroService.GetCompaniesAsync();
            return companies;
        }
    }
}
