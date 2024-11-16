using Application.Dtos;
using Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IValidateCredentialsService _validateCredentialsService;
        private readonly IValidateRefreshTokenService _validateRefreshTokenService;
        private readonly IRevokeJwtTokenService _revokeJwtTokenService;
        private readonly ILogger<LoginController> _logger;
        public LoginController(IValidateCredentialsService validateCredentialsService,
                               IValidateRefreshTokenService validateRefreshTokenService,
                               IRevokeJwtTokenService revokeJwtTokenService,
                               ILogger<LoginController> logger)
        {
            _validateCredentialsService = validateCredentialsService;
            _validateRefreshTokenService = validateRefreshTokenService;
            _revokeJwtTokenService = revokeJwtTokenService;
            _logger = logger;
        }

        [HttpPost("Authorize")]
        public async Task<IActionResult> Authorize([FromBody] LoginDto request)
        {
            _logger.LogInformation("Start Authorize - Resquest: {0}", JsonConvert.SerializeObject(request.Email));

            var authorizeUser = await _validateCredentialsService.AuthenticateUserService(request);
            if (authorizeUser == null)
                return Unauthorized();

            return Ok(authorizeUser);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenHistoryDto request)
        {
            _logger.LogInformation("Start RefreshToken - Resquest: {0}", JsonConvert.SerializeObject(request));

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(request.AccessToken);

            if (jwtToken.ValidTo > DateTime.UtcNow)
                throw new InvalidOperationException("The token has not expired.");

            string userIdClaim = jwtToken.Claims.First(x => x.Type == "IdUser").Value;
            request.UserId = int.Parse(userIdClaim);

            var result = await _validateRefreshTokenService.RenewTokenService(request);

            return Ok(result);
        }

        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevocacionToken(int userId)
        {
            _logger.LogInformation("Start RevokeToken - Resquest: {0}", JsonConvert.SerializeObject(userId));

            await _revokeJwtTokenService.InvalidateTokenService(userId);
            return Ok();
        }
    }
}
