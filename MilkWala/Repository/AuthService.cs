using Microsoft.IdentityModel.Tokens;
using MilkWala.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Utility;

namespace MilkWala.Repository
{
    public class AuthService : IAuthService
    {
        private readonly IOwnerRespository _ownerRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IOwnerRespository ownerRepository, IConfiguration configuration)
        {
            _ownerRepository = ownerRepository;
            _configuration = configuration;
        }

        public async Task<(string, string)> GenerateAndSendOtpAsync(string phone)
        {
            var owner = await _ownerRepository.GetOwnerByPhoneAsync(phone);
            if (owner == null)
                return ("User not found", "0");
            string JWTToken = GenerateJwtToken(owner.Phone, Get_configuration());
            // Generate OTP
            string otp = OtpGenerator.GenerateOtp(4); // Use the OTP generation method you provided

            // Update the owner with OTP and generation time
            owner.OTP = otp;
            owner.OTPGenTime = DateTime.Now;
            await _ownerRepository.UpdateOwnerAsync(owner);

            // TODO: Send OTP to the user via SMS or email

            return (otp, JWTToken); // For testing purposes, return OTP
        }

        public async Task<bool> VerifyOtpAsync(string phone, string otp)
        {
            var owner = await _ownerRepository.GetOwnerByPhoneAsync(phone);
            if (owner == null || owner.OTP != otp)
            {
                return false;
            }

            // Check OTP expiration
            var otpExpiryMinutes = _configuration.GetValue<int>("OtpExpiryMinutes");
            var otpGeneratedTime = owner.OTPGenTime ?? DateTime.MinValue;
            if ((DateTime.Now - otpGeneratedTime).TotalMinutes > otpExpiryMinutes)
            {
                return false;
            }

            return true;
        }

        public IConfiguration Get_configuration()
        {
            return _configuration;
        }

        public string GenerateJwtToken(string phone, IConfiguration _configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, phone),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["Jwt:ExpireMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
