using Newtonsoft.Json.Linq;

namespace Project.Domain.Entities
{
    public class FixedIncomeCalculatorEntity
    {
        public string Type { get; set; }
        public string Modality { get; set; }
        public float Profitability { get; set; }
        public int Months { get; set; }

        public FixedIncomeCalculatorEntity(string type, string modality, float profitability, int months)
        {
            Type = type;
            Modality = modality;
            Profitability = profitability;
            Months = months;
        }
    }
}
