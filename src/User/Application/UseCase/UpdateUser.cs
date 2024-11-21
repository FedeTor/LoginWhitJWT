using Application.Exceptions;
using Domain.Interfaces.IDto;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IUseCase;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Application.UseCase
{
    public class UpdateUser : IUpdateUser
    {
        private readonly IRepository _repository;
        private readonly ILogger<UpdateUser> _logger;

        public UpdateUser(IRepository repository, ILogger<UpdateUser> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task UpdateAsync(IUserDtoIn userDto)
        {
            _logger.LogInformation("Start SaveAsync - request: {0}", JsonConvert.SerializeObject(userDto));
            var user = await _repository.GetByIdAsync(userDto.Id);
            if (user == null)
                throw new UserNotFoundException();

            user.UpdatePartial(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password);

            await _repository.UpdateByIdAsync(user.Id, user);
        }
    }
}
