using Domain.Interfaces.IDto;

namespace Domain.Interfaces.IUseCase
{
    public interface IGetUser
    {

        Task<IUserDtoOut> GetAsync(int id);
    }
}
