using Application.Dtos;
using Application.Services.IServices;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.Services
{
    public class ServiceUpdateUser : IServiceUpdateUser
    {
        private readonly IUpdateUser _updateUser;
        private readonly ILogger<ServiceUpdateUser> _logger;

        public ServiceUpdateUser(IUpdateUser updateUser, ILogger<ServiceUpdateUser> logger)
        {
            _updateUser = updateUser;
            _logger = logger;
        }
        public async Task UpdateUserAsync(UserDtoIn userDto)
        {
            _logger.LogInformation("Start UpdateUserAsync - request: {0}", JsonConvert.SerializeObject(userDto));
            await _updateUser.UpdateAsync(userDto);
        }
    }
}
