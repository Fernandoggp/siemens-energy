using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Project.Domain.Exceptions;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Project.Application.Configurations
{
    public class HttpClientWrapper : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _projectUrl;
        private readonly string _servicoUrl;
        private readonly string _authenticationString;
        private readonly string _mscomunicacaointerna;

        public HttpClientWrapper(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _projectUrl = configuration["ApiUrls:ProjectUrl"];
            _servicoUrl = configuration["TokenConfiguration:AuthenticationPath"];
            _authenticationString = configuration["TokenConfiguration:AuthenticationString"];
            _mscomunicacaointerna = Environment.GetEnvironmentVariable("MsComunicacaoInterna");
        }

        public async Task<T> GetAsync<T>(string url)
        {
            var token = GetToken();

            var apiUrl = _projectUrl + url;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Result.access_token);
            var response = _httpClient.GetAsync(apiUrl).Result;

            if (response.IsSuccessStatusCode)
            {
                var retornoJson = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<T>(retornoJson);
                return retorno;
            }
            else
            {
                throw new ObjectValidationException($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task<T> PostAsync<T>(string url, object data)
        {
            var apiUrl = _mscomunicacaointerna + url;

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await _httpClient.PostAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                var retornoJson = await response.Content.ReadAsStringAsync();
                var retorno = JsonConvert.DeserializeObject<T>(retornoJson);
                return retorno;
            }
            else
            {
                throw new ObjectValidationException($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        public async Task<HttpStatusCode> PutAsync<T>(string url, object data)
        {
            var token = GetToken();

            var apiUrl = _projectUrl + url;
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.Result.access_token);

            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");


            var response = await _httpClient.PutAsync(apiUrl, content);

            if (response.IsSuccessStatusCode)
            {
                return response.StatusCode;
            }
            else
            {
                throw new ObjectValidationException($"Erro na requisição: {response.StatusCode} - {response.ReasonPhrase}");
            }
        }

        private async Task<TokenResource> GetToken()
        {
            var urlServico = _servicoUrl;
            var ApiInterna = _projectUrl + "v1";
            ApiInterna = ApiInterna.Replace("api/v1", "");
            ApiInterna = ApiInterna.Trim().ToLower();

            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ApiInterna);

            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, urlServico);
            tokenRequest.Headers.Add("resourceurl", urlServico);
            tokenRequest.Content = new StringContent(_authenticationString);
            tokenRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var tokenResponse = await httpClient.SendAsync(tokenRequest);

            if (!tokenResponse.IsSuccessStatusCode)
                throw new ArgumentException("Ocorreu um erro ao recuperar o token");

            var responseContent = await tokenResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TokenResource>(responseContent);
        }

        public class TokenResource
        {
            public string access_token { get; set; }

            public string token_type { get; set; }

            public int expires_in { get; set; }
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}
