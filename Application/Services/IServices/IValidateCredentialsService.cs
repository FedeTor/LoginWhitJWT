using Domain.Interfaces.IDto;

namespace Application.Services.IServices
{
    public interface IValidateCredentialsService
    {
        Task<IAuthorizeDto> AuthenticateUserService(ILoginDto request);
    }
}
