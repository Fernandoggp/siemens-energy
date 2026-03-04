using System.ComponentModel.DataAnnotations;

namespace Project.Api.ViewModel.Dto.Portfolio
{
    public class CreateAssetDto
    {
        [Required]
        public string name { get; set; }
        [Required]
        public float value { get; set; }
        public int desiredPercentage { get; set; }
        [Required]
        public string userId { get; set; }
    }
}
