using System.Text.Json.Serialization;

namespace MS.Hub.Application.Dtos
{
    public class UploadFilesDto
    {
        [JsonPropertyName("messages")]
        public List<string> messages { get; set; }

        [JsonPropertyName("data")]
        public List<string> data { get; set; }
    }
}
