using Microsoft.Extensions.Configuration;
using RestSharp;
using System;

namespace Project.Application.Base
{
    public class AsaasRequestBase
    {
        private readonly IConfiguration _configuration;

        public AsaasRequestBase(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public (RestClient client, RestRequest request) CreateAsaasRequest(string endpoint, Method method)
        {
            var baseUrl = _configuration["Asaas:BaseUrl"];
            var token = _configuration["Asaas:ApiKey"]; 

            var client = new RestClient(new RestClientOptions(baseUrl));
            var request = new RestRequest(endpoint, method);

            request.AddHeader("accept", "application/json");
            request.AddHeader("access_token", token);

            return (client, request);
        }
    }
}
