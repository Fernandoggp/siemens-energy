namespace Project.Domain.Entities
{
    public class AssetEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Value { get; set; }
        public int DesiredPercentage { get; set; }
        public string UserId { get; set; }
    }
}
