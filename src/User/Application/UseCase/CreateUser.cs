using Application.Exceptions;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class CreateUser : ICreateUser
    {
        private readonly IRepository _repository;
        private readonly ILogger<CreateUser> _logger;

        public CreateUser(IRepository repository, ILogger<CreateUser> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task SaveAsync(IUserDtoIn createUserDto)
        {
            _logger.LogInformation("Start SaveAsync - request: {0}", JsonConvert.SerializeObject(createUserDto));
            var existingUser = await _repository.FindAllAsync(u => u.Email == createUserDto.Email && u.Active == true);
            if (existingUser.Any())
                throw new EmailAlreadyRegisteredException();

            var user = new User(
                createUserDto.FirstName,
                createUserDto.LastName,
                createUserDto.Email,
                createUserDto.Password);

            await _repository.AddAsync(user);
        }
    }
}
