using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;

namespace ECommerceBackendSystem.Application.Abstractions.Services.TokenService;

public interface ITokenService
{
    ServiceResponse<string> BuildToken(string userId);
    ServiceResponse<bool> ValidateToken(string token);
}
