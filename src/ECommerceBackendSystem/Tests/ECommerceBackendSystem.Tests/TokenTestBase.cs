using ECommerceBackendSystem.API.Models.Tokens;

namespace ECommerceBackendSystem.Tests;

public partial class TestBase
{
    public GetTokenRequest CreateAGetTokenRequest(Guid? userId = default)
    {
        return new GetTokenRequest
        {
            UserId = userId == default ? Guid.NewGuid().ToString() : userId.ToString()
        };
    }

    public ValidateTokenRequest CreateAValidateTokenRequest(string token = null)
    {
        return new ValidateTokenRequest()
        {
            Token = token == null ? AString() : token
        };
    }
}