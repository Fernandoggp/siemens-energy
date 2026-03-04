using Microsoft.Extensions.Configuration;
using RestSharp;

namespace Project.Application.Base
{
    public class CedroRequestBase
    {
        private readonly IConfiguration _configuration;

        public CedroRequestBase(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public (RestClient client, RestRequest request) CreateCedroRequest(string endpoint, Method method)
        {
            var baseUrl = _configuration["Cedro:BaseUrl"]; 
            var token = _configuration["Cedro:ApiKey"]; 

            var client = new RestClient(new RestClientOptions(baseUrl));
            var request = new RestRequest(endpoint, method);

            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", token);

            return (client, request);
        }
    }
}
