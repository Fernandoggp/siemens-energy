using Newtonsoft.Json.Linq;
using Project.Application.Base;
using Project.Application.Dtos;
using Project.Domain.Exceptions;
using Project.Domain.Interfaces.Services;
using RestSharp;
using System.Net;

namespace Project.Application.Services
{
    public class CedroService : ICedroService
    {
        private readonly CedroRequestBase _cedroRequestBase;

        public CedroService(CedroRequestBase cedroRequestBase)
        {
            _cedroRequestBase = cedroRequestBase ?? throw new ArgumentNullException(nameof(cedroRequestBase));
        }

        public async Task<dynamic> GetCompaniesAsync()
        {
            var (client, request) = _cedroRequestBase.CreateCedroRequest("/companies", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                var companies = json["data"]?.ToObject<List<CompaniesDto>>();
                return companies;
            }
            else
            {
                throw new ObjectValidationException("Erro ao processar resposta da API da Cedro.");
            }
        }

        public async Task<dynamic> GetCompanyAsync(string ticker)
        {
            var (client, request) = _cedroRequestBase.CreateCedroRequest($"/companies/{ticker}", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                var company = json["data"]?.ToObject<CompanyDto>();
                return company;
            }
            else
            {
                throw new ObjectValidationException("Erro ao processar resposta da API da Cedro.");
            }
        }

        public async Task<dynamic> GetCompanyRatiosAsync(string ticker)
        {
            var (client, request) = _cedroRequestBase.CreateCedroRequest($"/companies/{ticker}/ratios", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                var company = json["data"]?.ToObject<CompanyRatiosDto>();
                return company;
            }
            else
            {
                throw new ObjectValidationException("Erro ao processar resposta da API da Cedro.");
            }
        }

        public async Task<dynamic> GetCompanyRatiosValuationAsync(string ticker)
        {
            var (client, request) = _cedroRequestBase.CreateCedroRequest($"/companies/{ticker}/ratios/valuation?publish_date=2024-12-31T00%3A00%3A00Z", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                var company = json["data"]?.ToObject<CompanyRatiosValuationDto>();
                return company;
            }
            else
            {
                throw new ObjectValidationException("Erro ao processar resposta da API da Cedro.");
            }
        }

        public async Task<dynamic> GetCompanyRawReportsAsync(string ticker)
        {
            var (client, request) = _cedroRequestBase.CreateCedroRequest($"/companies/{ticker}/raw-reports?aggregation=CONSOLIDATED&flat=true", Method.Get);
            var response = await client.ExecuteAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = JObject.Parse(response.Content);
                var company = json["data"]?.ToObject<CompanyRawReportsDto>();
                return company;
            }
            else
            {
                throw new ObjectValidationException("Erro ao processar resposta da API da Cedro.");
            }
        }
    }

}
