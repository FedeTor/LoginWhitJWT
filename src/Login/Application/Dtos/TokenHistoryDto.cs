using Domain.Interfaces.IDto;

namespace Application.Dtos
{
    public class TokenHistoryDto : ITokenHistoryDto
    {
        public int Id { get; private set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime TokenCreatedDate { get; private set; }
        public DateTime TokenExpiratedDate { get; private set; }
        public int UserId { get; set; }

        public TokenHistoryDto() { }

        public TokenHistoryDto(string accessToken, string refreshToken, DateTime tokenCreatedDate, DateTime tokenExpiratedDate, int userId)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenCreatedDate = tokenCreatedDate;
            TokenExpiratedDate = tokenExpiratedDate;
            UserId = userId;
        }
    }
}
