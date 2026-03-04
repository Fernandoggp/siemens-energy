using Microsoft.AspNetCore.Http;

namespace Project.Domain.Interfaces.Http
{
    public interface IHttpClientService
    {
        Task<string> GetAsync(string url, IDictionary<string, string> headers);
        Task<HttpResponseMessage> GetFileAsync(string url, IDictionary<string, string> headers);
        Task<string> PostAsync<T>(string url, T data, IDictionary<string, string> headers);
        Task<string> PostMultipartAsync(string url, List<IFormFile> files, IDictionary<string, string> headers);
        Task<string> PutAsync<T>(string url, T data, IDictionary<string, string> headers);
        Task<string> DeleteAsync(string url, IDictionary<string, string> headers);
    }
}
