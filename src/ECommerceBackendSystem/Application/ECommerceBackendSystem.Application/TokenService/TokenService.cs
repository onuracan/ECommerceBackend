using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.Base;
using ECommerceBackendSystem.Application.Abstractions.Services.TokenService;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ECommerceBackendSystem.Application.TokenService;

public class TokenService(IConfiguration configuration,
                          IServiceResponseHelper serviceResponseHelper) : ITokenService
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IServiceResponseHelper _serviceResponseHelper = serviceResponseHelper;

    public ServiceResponse<string> BuildToken(string userId)
    {
        var claims = new List<Claim>()
        {
            new Claim("UserId", userId),
        };

        var newExpireTime = DateTime.UtcNow.AddHours(int.Parse(this._configuration["JwtIssuerSettings:ExpireTime"]));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = newExpireTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["JwtIssuerSettings:Key"].ToString())), SecurityAlgorithms.HmacSha256Signature),
            Issuer = this._configuration["JwtIssuerSettings:Issuer"],
            Audience = this._configuration["JwtIssuerSettings:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return this._serviceResponseHelper.Success<string>(jwt);
    }

    public ServiceResponse<bool> ValidateToken(string token)
    {
        var mySecret = Encoding.UTF8.GetBytes(this._configuration["JwtIssuerSettings:Key"].ToString());
        var mySecurityKey = new SymmetricSecurityKey(mySecret);
        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken validatedToken = null;

        tokenHandler.ValidateToken(token,
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = this._configuration["JwtIssuerSettings:Issuer"].ToString(),
            ValidAudience = this._configuration["JwtIssuerSettings:Issuer"].ToString(),
            IssuerSigningKey = mySecurityKey,
        }, out validatedToken);

        if (validatedToken == null)
            return this._serviceResponseHelper.Fail<bool>("Token is not valid", System.Net.HttpStatusCode.Unauthorized);
        else
            return this._serviceResponseHelper.Success<bool>(true, "Token is valid");
    }
}
