using Domain.Interfaces.IDto;

namespace Application.Services.IServices
{
    public interface IServiceGetUser
    {
        Task<IUserDtoOut> GetUserAsync(int id);
    }
}
