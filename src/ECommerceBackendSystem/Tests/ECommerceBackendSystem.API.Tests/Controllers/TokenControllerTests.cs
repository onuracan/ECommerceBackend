using ECommerceBackendSystem.API.Controllers;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.TokenService;
using ECommerceBackendSystem.Tests;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ECommerceBackendSystem.API.Tests.Controllers;

public class TokenControllerTests:TestBase
{
    private readonly Mock<ITokenService> _tokenServiceMock = new();

    [Fact]
    public void BuildToken_ReturnsOk_WhenSuccessful()
    {
        var userId = Guid.NewGuid();
        var response = new ServiceResponse<string> { IsSuccessful = true, Data = "token" };

        _tokenServiceMock.Setup(x => x.BuildToken(userId.ToString())).Returns(response);

        var controller = new TokenController(_tokenServiceMock.Object);

        var result = controller.BuidToken(CreateAGetTokenRequest(userId));

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("token", ((ServiceResponse<string>)okResult.Value).Data);
    }

    [Fact]
    public void BuildToken_ReturnsBadRequest_WhenNotSuccessful()
    {
        var userId = Guid.NewGuid();
        var response = new ServiceResponse<string> { IsSuccessful = false, Message = "Error" };

        _tokenServiceMock.Setup(x => x.BuildToken(userId.ToString())).Returns(response);

        var controller = new TokenController(_tokenServiceMock.Object);

        var result = controller.BuidToken(CreateAGetTokenRequest(userId));

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public void ValidateToken_ReturnsOk_WhenSuccessful()
    {
        var token = "token";
        var response = new ServiceResponse<bool> { IsSuccessful = true, Data = true };

        _tokenServiceMock.Setup(x => x.ValidateToken(token)).Returns(response);

        var controller = new TokenController(_tokenServiceMock.Object);

        var result = controller.ValidateToken(CreateAValidateTokenRequest(token));

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.True(((ServiceResponse<bool>)okResult.Value).Data);
    }

    [Fact]
    public void ValidateToken_ReturnsUnauthorized_WhenNotSuccessful()
    {
        var token = "token";
        var response = new ServiceResponse<bool> { IsSuccessful = false, Message = "Unauthorized" };

        _tokenServiceMock.Setup(x => x.ValidateToken(token)).Returns(response);

        var controller = new TokenController(_tokenServiceMock.Object);

        var result = controller.ValidateToken(CreateAValidateTokenRequest(token));

        Assert.IsType<UnauthorizedObjectResult>(result);
    }
}
