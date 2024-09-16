namespace MilkWala.Models
{
    public class MWOwner
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string? Email { get; set; }

        public bool IsActive { get; set; }
        public string? OTP { get; set; }
        public DateTime? OTPGenTime { get; set; }
    }
}
