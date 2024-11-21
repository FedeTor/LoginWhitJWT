using Application.Dtos;

namespace Application.Services.IServices
{
    public interface IServiceCreateUser
    {
        Task CreateUserAsync(UserDtoIn user);
    }
}
