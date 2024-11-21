using Domain.Interfaces.IDto;

namespace Domain.Interfaces.IUseCase
{
    public interface ICreateUser
    {
        Task SaveAsync(IUserDtoIn createUserDto);
        
    }
}
