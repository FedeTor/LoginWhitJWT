using Domain.Interfaces.IDto;

namespace Application.Services.IServices
{
    public interface IServiceGetAllUsers
    {
        Task<IEnumerable<IUserDtoOut>> GetAllUsersAsync(bool onlyActive);
    }
}
