using ECommerceBackendSystem.API.Filters;
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
        /// Kullanıcıya JWT token üretir.
        /// </summary>
        /// <param name="userId">Kullanıcı Id'si</param>
        [HttpGet("{userId}")]
        [ValidateGuid("userId")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult BuidToken([FromRoute] string userId)
        {
            var response = this._tokenService.BuildToken(userId);

            if (!response.IsSuccessful)
                return BadRequest(response);

            return Ok(response);
        }

        /// <summary>
        /// Kullanıcının JWT token'ınını doğrular.
        /// </summary>
        /// <param name="token">Kullanıcı token'ı</param>
        [HttpGet("validate/{token}")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult ValidateToken([FromRoute] string token)
        {
            var response = this._tokenService.ValidateToken(token);

            if (!response.IsSuccessful)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
