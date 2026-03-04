using Microsoft.AspNetCore.Http;
using Project.Domain.Interfaces.Http;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Project.Application.Services.Http
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private void AddHeaders(HttpRequestMessage request, IDictionary<string, string> headers)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
        }

        public async Task<string> GetAsync(string url, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<HttpResponseMessage> GetFileAsync(string url, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<string> PostAsync<T>(string url, T data, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostMultipartAsync(string url, List<IFormFile> files, IDictionary<string, string> headers = null)
        {
            // Cria o conteúdo multipart
            var multipartContent = new MultipartFormDataContent();

            // Adiciona cada arquivo ao conteúdo multipart
            foreach (var file in files)
            {
                var streamContent = new StreamContent(file.OpenReadStream());
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);

                // Adiciona o arquivo, o nome do arquivo e o campo 'binary'
                multipartContent.Add(streamContent, "binary", file.FileName);
            }

            // Cria a requisição HTTP
            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = multipartContent
            };

            // Adiciona cabeçalhos (se necessário)
            AddHeaders(request, headers);

            // Envia a requisição
            var response = await _httpClient.SendAsync(request);

            // Verifica se houve sucesso na requisição
            response.EnsureSuccessStatusCode();

            // Retorna a resposta como string
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PutAsync<T>(string url, T data, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json")
            };
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteAsync(string url, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> PostXmlAsync<T>(string url, T data, IDictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, data);
                request.Content = new StringContent(stringWriter.ToString(), Encoding.UTF8, "application/xml");
            }
            AddHeaders(request, headers);
            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
