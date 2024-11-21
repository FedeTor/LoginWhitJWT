using Application.Dtos;

namespace Application.Services.IServices
{
    public interface IServiceUpdateUser
    {
        Task UpdateUserAsync(UserDtoIn user);
    }
}
