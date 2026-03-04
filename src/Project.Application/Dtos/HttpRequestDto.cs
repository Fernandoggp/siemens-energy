using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Project.Application.Dtos
{
    public class ApiResponse
    {
        public List<Result> Results { get; set; }
        public DateTime RequestedAt { get; set; }
        public string Took { get; set; }
    }
    public class Result
    {
        public string Name { get; set; }
        public SummaryProfile SummaryProfile { get; set; }
        public DefaultKeyStatistics DefaultKeyStatistics { get; set; }
    }
    public class SummaryProfile
    {
        public string Sector { get; set; }
    }
    public class DefaultKeyStatistics
    {
        public int EnterpriseValue { get; set; }
    }

}
