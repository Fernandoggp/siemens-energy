using System.Text.Json.Serialization;

namespace MS.Hub.Application.Dtos
{
    public class CreateAccountOnErpDto
    {
        [JsonPropertyName("CodigoTerceiro")]
        public string thirdCode { get; set; }

        [JsonPropertyName("CodigoFichaCadastral")]
        public string registerFormCode { get; set; }

        [JsonPropertyName("Status")]
        public string status { get; set; }

        [JsonPropertyName("Mensagem")]
        public string message { get; set; }
    }
}
