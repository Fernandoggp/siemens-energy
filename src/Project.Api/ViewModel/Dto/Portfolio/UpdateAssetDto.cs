using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Portfolio
{
    public class UpdateAssetDto
    {
        [Required]
        public Guid id { get; set; }
        public string name { get; set; }
        public float value { get; set; }
        public int desiredPercentage { get; set; }
        [Required]
        public string user_id { get; set; }
    }
}
