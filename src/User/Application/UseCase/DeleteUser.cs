using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class DeleteUser : IDeleteUser
    {
        private readonly IRepository _repository;
        private readonly ILogger<DeleteUser> _logger;

        public DeleteUser(IRepository repository, ILogger<DeleteUser> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task DeleteAsync(int id)
        {
            _logger.LogInformation("Start DeleteAsync - request: {0}", id);
            var user = await _repository.FindFirstOrDefaultAsync(u => u.Id == id && u.Active);

            if (user == null)
                throw new UserInactiveException();

            user.Deactivate();

            await _repository.UpdateByIdAsync(id, user);
        }
    }
}
