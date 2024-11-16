using Domain.Interfaces.IDto;

namespace Domain.Interfaces.IUseCase
{
    public interface IValidateCredentials
    {
        public Task<IAuthorizeDto> AuthenticateUser(ILoginDto request);
    }
}
