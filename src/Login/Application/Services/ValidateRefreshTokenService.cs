using Application.Dtos;
using Application.Services.IServices;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ValidateRefreshTokenService : IValidateRefreshTokenService
    {
        private readonly IValidateRefreshToken _validateRefreshToken;
        private readonly ILogger<ValidateRefreshTokenService> _logger;

        public ValidateRefreshTokenService(IValidateRefreshToken validateRefreshToken, ILogger<ValidateRefreshTokenService> logger)
        {
            _validateRefreshToken = validateRefreshToken;
            _logger = logger;
        }

        public async Task<IAuthorizeDto> RenewTokenService(TokenHistoryDto request)
        {
            _logger.LogInformation("Start RenewTokenService - Request: {0}", request);

            return await _validateRefreshToken.UpdateToken(request);
        }
    }
}
