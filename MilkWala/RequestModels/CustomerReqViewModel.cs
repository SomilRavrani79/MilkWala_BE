using System.ComponentModel.DataAnnotations;

namespace MilkWala.RequestModels
{
    public class CustomerReqViewModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]

        public string Phone { get; set; } = null!;

        public string? Email { get; set; }
        public bool IsActive { get; set; }
    }
}
