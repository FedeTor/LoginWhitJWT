using Application.Services.IServices;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceGetUser : IServiceGetUser
    {
        private readonly IGetUser _getUser;
        private readonly ILogger<ServiceGetUser> _logger;

        public ServiceGetUser(IGetUser getUser, ILogger<ServiceGetUser> logger)
        {
            _getUser = getUser;
            _logger = logger;
        }

        public async Task<IUserDtoOut> GetUserAsync(int id)
        {
            _logger.LogInformation("Start GetUserAsync - request: {0}", id);
            return await _getUser.GetAsync(id);
        }
    }
}
