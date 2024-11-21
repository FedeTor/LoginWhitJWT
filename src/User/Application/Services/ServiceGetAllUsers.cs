using Application.Dtos;
using Application.Services.IServices;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceGetAllUsers : IServiceGetAllUsers
    {
        private readonly IGetAllUsers _getAllUsers;
        private readonly ILogger<ServiceGetAllUsers> _logger;

        public ServiceGetAllUsers(IGetAllUsers getAllUsers, ILogger<ServiceGetAllUsers> logger)
        {
            _getAllUsers = getAllUsers;
            _logger = logger;
        }

        public async Task<IEnumerable<IUserDtoOut>> GetAllUsersAsync(bool onlyActive)
        {
            _logger.LogInformation("Start GetAllUsersAsync - request: {0}", onlyActive);
            return await _getAllUsers.GetAll(onlyActive);
        }
    }
}
