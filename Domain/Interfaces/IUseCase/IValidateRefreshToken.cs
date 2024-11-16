using Domain.Interfaces.IDto;

namespace Domain.Interfaces.IUseCase
{
    public interface IValidateRefreshToken
    {
        public Task<IAuthorizeDto> UpdateToken(ITokenHistoryDto request);
    }
}
