using Application.Dtos;
using Application.Exceptions;
using Domain.Entities;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;

namespace Application.UseCase
{
    public class GetUser : IGetUser
    {
        private readonly IRepository _repository;
        private readonly ILogger<GetUser> _logger;

        public GetUser(IRepository repository, ILogger<GetUser> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IUserDtoOut> GetAsync(int id)
        {
            _logger.LogInformation("Start GetAsync - request: {0}", id);
            var user = await _repository.GetByIdAsync(id);
            return user == null
                ? throw new UserNotFoundException()
                : new UserDtoOut
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Active = user.Active,
                    CreatedDate = DateTime.Now
                };
        }
    }
}
