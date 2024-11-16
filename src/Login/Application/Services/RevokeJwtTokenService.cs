using Application.Services.IServices;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Services
{
    public class RevokeJwtTokenService : IRevokeJwtTokenService
    {
        private readonly IRevokeJwtToken _revokeJwtToken;
        private readonly ILogger<RevokeJwtTokenService> _logger;

        public RevokeJwtTokenService(IRevokeJwtToken revokeJwtToken, ILogger<RevokeJwtTokenService> logger)
        {
            _revokeJwtToken = revokeJwtToken;
            _logger = logger;
        }

        public async Task<bool> InvalidateTokenService(int id)
        {
            _logger.LogInformation("Start InvalidateToken - Resquest: {0}", JsonConvert.SerializeObject(id));

            return await _revokeJwtToken.InvalidateToken(id);
        }
    }
}
