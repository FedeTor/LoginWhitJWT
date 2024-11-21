using Application.Dtos;
using Application.Helpers;
using Application.Security;
using Domain.Entities;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.UseCase
{
    public class ValidateRefreshToken : IValidateRefreshToken
    {
        private IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<TokenHistory> _historyTokenRepository;
        private readonly ILogger<ValidateRefreshToken> _logger;

        public ValidateRefreshToken(IConfiguration configuration,
                                    IRepository<User> userRepository,
                                    IRepository<TokenHistory> historyTokenRepository,
                                    ILogger<ValidateRefreshToken> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _historyTokenRepository = historyTokenRepository;
            _logger = logger;
        }

        public async Task<IAuthorizeDto> UpdateToken(ITokenHistoryDto request)
        {
            _logger.LogInformation("Start UpdateToken - Request: {0}", request.UserId);

            var tokenExpirationTime = int.Parse(_configuration.GetSection("JwtSettings")["ExpirationToken"]);

            var existingToken = await _historyTokenRepository.FindAsync(t => t.UserId == request.UserId && t.AccessToken == request.AccessToken);
            if (existingToken == null)
                throw new UnauthorizedAccessException("Token not found or invalid.");

            var user = await _userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                throw new UnauthorizedAccessException("User not found.");

            var mappingObject = TokenHistory.Create(
                id: request.Id,
                accessToken: existingToken.AccessToken,
                refreshToken: existingToken.RefreshToken,
                tokenCreatedDate: existingToken.TokenCreatedDate,
                tokenExpiratedDate: existingToken.TokenExpiratedDate,
                userId: existingToken.UserId
            );

            var newTokenHistory = await GenerateAndSaveNewTokensAsync(user, tokenExpirationTime, mappingObject);

            return new AuthorizeDto
            {
                AccessToken = newTokenHistory.AccessToken,
                RefreshToken = newTokenHistory.RefreshToken
            };
        }

        private async Task<TokenHistory> GenerateAndSaveNewTokensAsync(User user, int tokenExpirationTime, TokenHistory mappingObject)
        {
            var generateToken = new TokenService(_configuration);
            var newAccessToken = generateToken.GenerateAccessToken(user.Id.ToString());
            var newRefreshToken = generateToken.GenerateRefreshToken();

            var argentinaTime = DateTimeHelper.GetArgentinaTime();

            mappingObject.Update(
                newAccessToken, 
                newRefreshToken,
                argentinaTime,
                argentinaTime.AddMinutes(tokenExpirationTime)
                );

            await _historyTokenRepository.AddAsync(mappingObject);

            return await _historyTokenRepository.GetByIdAsync(mappingObject.Id);
        }
    }
}
