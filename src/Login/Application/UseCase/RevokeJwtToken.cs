using Domain.Entities;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class RevokeJwtToken : IRevokeJwtToken
    {
        private readonly IRepository<TokenHistory> _historyTokenRepository;
        private readonly ILogger<RevokeJwtToken> _logger;

        public RevokeJwtToken(IRepository<TokenHistory> historyTokenRepository, ILogger<RevokeJwtToken> logger)
        {
            _historyTokenRepository = historyTokenRepository;
            _logger = logger;
        }

        public async Task<bool> InvalidateToken(int id)
        {
            _logger.LogInformation("Start InvalidateTokenService - Resquest: {0}", JsonConvert.SerializeObject(id));

            var tokenIds = (await _historyTokenRepository.FindAllAsync(t => t.UserId == id)).Select(t => t.Id).ToList();

            if (!tokenIds.Any())
                throw new KeyNotFoundException($"No tokens found for User ID: {id}");

            await _historyTokenRepository.DeleteRangeAsync(tokenIds);

            return true;
        }
    }
}
