namespace MilkWala.IRepository
{
    public interface IAuthService
    {
        Task<(string, string)> GenerateAndSendOtpAsync(string phone);
        Task<bool> VerifyOtpAsync(string phone, string otp);
    }
}
