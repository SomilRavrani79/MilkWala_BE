using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public class JwtMiddlewareAttribute : ActionFilterAttribute
{
    private readonly RequestDelegate _next;
    private readonly JwtSettings _jwtSettings;

    public JwtMiddlewareAttribute(RequestDelegate _next, IOptions<JwtSettings> jwtSettings)
    {
        this._next = _next;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task Invoke(HttpContext context)
    {

        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
            //Validate Token
            AttachUserToContext(context, token);
        _next(context);
    }

    private void AttachUserToContext(HttpContext context, string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Issuer
            }, out SecurityToken validateToken);


            var jwtToken = (JwtSecurityToken)validateToken;
            var userId = int.Parse(jwtToken.Claims.FirstOrDefault(_ => _.Type == "Id").Value);

        }
        catch (Exception ex)
        {


        }
    }
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpireMinutes { get; set; }
    }

}
