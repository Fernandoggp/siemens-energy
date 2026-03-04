using System.ComponentModel.DataAnnotations;

namespace Project.Api.Bases
{
    public abstract class EntityBaseModelView
    {
        [Required]
        public Guid Id { get; set; }
    }
}
