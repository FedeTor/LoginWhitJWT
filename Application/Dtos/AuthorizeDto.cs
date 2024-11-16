using Domain.Interfaces.IDto;

namespace Application.Dtos
{
    public class AuthorizeDto : IAuthorizeDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
