using System.Text.Json.Serialization;

namespace Project.Api.ViewModel.Dto.Sector
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AddSector
    {
        perennial,
        intermediary,
        unstable
    }
}

