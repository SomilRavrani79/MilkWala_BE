using Microsoft.AspNetCore.Mvc;
using MilkWala.IRepository;
using MilkWala.Models;

[ApiController]

[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login( LoginReq req)
    {
        try
        {
            var otp = await _authService.GenerateAndSendOtpAsync(req.mobileNumber);
            return Ok(new { OTP = otp.Item1, Token = otp.Item2 });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("verify-otp")]
    public async Task<ApiResponse> VerifyOtp([FromBody] OtpVerificationRequest request)
    {
        try
        {
            bool isValid = await _authService.VerifyOtpAsync(request.Phone, request.Otp);
            if (isValid)
            {
                return new ApiResponse(200, true, "OTP verified successfully");
            }
            return new ApiResponse(401, false, "Invalid or expired OTP");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}


