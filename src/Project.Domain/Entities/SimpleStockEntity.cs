namespace Project.Domain.Entities
{
    public class SimpleStockEntity
    {
        public string Code { get; set; }
        public decimal Value { get; set; }

        public SimpleStockEntity(string code, decimal value)
        {
            Code = code;
            Value = value;
        }
    }
}
