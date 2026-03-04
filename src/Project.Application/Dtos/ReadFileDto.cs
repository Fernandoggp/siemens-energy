using System.Text.Json.Serialization;

namespace MS.Hub.Application.Dtos
{
    public class ReadFileDto
    {
        [JsonPropertyName("message")]
        public string message { get; set; }

        [JsonPropertyName("data")]
        public List<string> data { get; set; }
    }
}
