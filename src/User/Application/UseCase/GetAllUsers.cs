using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace Application.UseCase
{
    public class GetAllUsers : IGetAllUsers
    {
        private readonly IRepository _repository;
        private readonly ILogger<GetAllUsers> _logger;

        public GetAllUsers(IRepository repository, ILogger<GetAllUsers> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<IUserDtoOut>> GetAll(bool onlyActive)
        {
            _logger.LogInformation("Start GetAll - request: {0}", onlyActive);
            Expression<Func<User, bool>> filter = user => !onlyActive || user.Active;

            var users = await _repository.FindAllAsync(filter);

            var orderedUsers = users.OrderByDescending(user => user.Active);

            return orderedUsers.Select(user => new UserDtoOut
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Active = user.Active,
                CreatedDate = DateTime.Now
            });
        }
    }
}
