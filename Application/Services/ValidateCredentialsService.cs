using Application.Services.IServices;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace Application.Services
{
    public class ValidateCredentialsService : IValidateCredentialsService
    {
        private readonly IValidateCredentials _validateCredentials;
        private readonly ILogger<ValidateCredentialsService> _logger;

        public ValidateCredentialsService(IValidateCredentials validateCredentials, ILogger<ValidateCredentialsService> logger)
        {
            _validateCredentials = validateCredentials;
            _logger = logger;
        }

        public async Task<IAuthorizeDto> AuthenticateUserService(ILoginDto request)
        {
            _logger.LogInformation("Start AuthenticateUser - Resquest: {0}", JsonConvert.SerializeObject(request.Email));

            return await _validateCredentials.AuthenticateUser(request);
        }
    }
}
