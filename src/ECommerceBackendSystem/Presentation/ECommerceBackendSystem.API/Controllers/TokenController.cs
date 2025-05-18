using ECommerceBackendSystem.API.Models.Tokens;
using ECommerceBackendSystem.Application.Abstractions.Dtos.ServiceResponse;
using ECommerceBackendSystem.Application.Abstractions.Services.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackendSystem.API.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class TokenController(ITokenService tokenService) : ControllerBase
    {
        private readonly ITokenService _tokenService = tokenService;

        /// <summary>
        /// Kullanýcýya JWT token üretir.
        /// </summary>
        /// <param name="request">Kullanýcý ID'si</param>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult BuidToken([FromRoute] GetTokenRequest request)
        {
            var response = this._tokenService.BuildToken(request.UserId);

            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Kullanýcýnýn JWT token'ýnýný doðrular.
        /// </summary>
        /// <param name="request">Kullanýcý token'ý</param>
        [HttpGet("validate/{token}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult ValidateToken([FromRoute] ValidateTokenRequest request)
        {
            var response = this._tokenService.ValidateToken(request.Token);

            if (!response.IsSuccessful)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
