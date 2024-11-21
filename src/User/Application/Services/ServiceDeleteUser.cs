using Application.Services.IServices;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ServiceDeleteUser : IServiceDeleteUser
    {
        private IDeleteUser _deleteUser;
        private readonly ILogger<ServiceDeleteUser> _logger;

        public ServiceDeleteUser(IDeleteUser deleteUser, ILogger<ServiceDeleteUser> logger)
        {
            _deleteUser = deleteUser;
            _logger = logger;
        }

        public async Task DeleteUserAsync(int id)
        {
            _logger.LogInformation("Start DeleteUserAsync - request: {0}", id);
            await _deleteUser.DeleteAsync(id);
        }
    }
}
