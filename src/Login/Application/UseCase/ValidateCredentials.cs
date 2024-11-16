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
    public class ValidateCredentials : IValidateCredentials
    {
        private IConfiguration _configuration;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<TokenHistory> _historyTokenRepository;
        private readonly ILogger<ValidateCredentials> _logger;

        public ValidateCredentials(IConfiguration configuration,
                                   IRepository<User> userRepository,
                                   IRepository<TokenHistory> historyTokenRepository,
                                   ILogger<ValidateCredentials> logger)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _historyTokenRepository = historyTokenRepository;
            _logger = logger;
        }

        public async Task<IAuthorizeDto> AuthenticateUser(ILoginDto request)
        {
            _logger.LogInformation("Start AuthenticateUserService - Request: {0}", request.Email);

            var user = await _userRepository.FindAsync(u => u.Email == request.Email && u.Active == true);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid credentials.");

            string passwordSaved = EncryptionService.EncryptPassword(request.Password, user.Salt);
            if (user.Hash != passwordSaved)
                throw new UnauthorizedAccessException("Invalid credentials.");

            var tokenHistoryData = await GenerateAndSaveTokensAsync(user);

            return new AuthorizeDto
            {
                AccessToken = tokenHistoryData.AccessToken,
                RefreshToken = tokenHistoryData.RefreshToken
            };
        }

        private async Task<TokenHistoryDto> GenerateAndSaveTokensAsync(User request)
        {
            var tokenExpirationTime = int.Parse(_configuration.GetSection("JwtSettings")["ExpirationToken"]);

            var tokenCreatedOn = DateTimeHelper.GetArgentinaTime();
            var tokenExpiratedOn = tokenCreatedOn.AddMinutes(tokenExpirationTime);

            var generateToken = new TokenService(_configuration);

            var tokenHistoryData = new TokenHistoryDto(
                accessToken: generateToken.GenerateAccessToken(request.Id.ToString(), request.RoleId.ToString()),
                refreshToken: generateToken.GenerateRefreshToken(),
                tokenCreatedDate: tokenCreatedOn,
                tokenExpiratedDate: tokenExpiratedOn,
                userId: request.Id
            );

            var objectMapping = TokenHistory.Create(
                id: tokenHistoryData.Id,
                accessToken: tokenHistoryData.AccessToken,
                refreshToken: tokenHistoryData.RefreshToken,
                tokenCreatedDate: tokenHistoryData.TokenCreatedDate,
                tokenExpiratedDate: tokenHistoryData.TokenExpiratedDate,
                userId: tokenHistoryData.UserId
            );

            await _historyTokenRepository.AddAsync(objectMapping);

            return tokenHistoryData;
        }
    }
}
