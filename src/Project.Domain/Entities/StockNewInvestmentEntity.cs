namespace Project.Domain.Entities
{
    public class StockNewInvestmentEntity
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public int DesiredPercentage { get; set; }

        public StockNewInvestmentEntity(string name, decimal value, int desiredPercentage)
        {
            Name = name;
            Value = value;
            DesiredPercentage = desiredPercentage;
        }
    }
}
