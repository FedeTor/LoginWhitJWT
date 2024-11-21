using Application.Dtos;
using Application.Services.IServices;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Services
{
    public class ServiceCreateUser : IServiceCreateUser
    {
        private readonly ICreateUser _createUser;
        private readonly ILogger<ServiceCreateUser> _logger;

        public ServiceCreateUser(ICreateUser createUser, ILogger<ServiceCreateUser> logger)
        {
            _createUser = createUser;
            _logger = logger;
        }
        public async Task CreateUserAsync(UserDtoIn user)
        {
            _logger.LogInformation("Start CreateUserAsync - request: {0}", JsonConvert.SerializeObject(user));
            await _createUser.SaveAsync(user);
        }
    }
}
