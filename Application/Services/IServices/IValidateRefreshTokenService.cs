using Application.Dtos;
using Domain.Interfaces.IDto;

namespace Application.Services.IServices
{
    public interface IValidateRefreshTokenService
    {
        Task<IAuthorizeDto> RenewTokenService(TokenHistoryDto request);
    }
}
