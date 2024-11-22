using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// La interfaz <see cref="IIdentifiable"/> permite acceder a un identificador único.
    /// </summary>
    public interface IIdentifiable
    {
        int Id { get; }
    }

    public class TokenHistory : IIdentifiable
    {
        public int Id { get; private set; }
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime TokenCreatedDate { get; private set; }
        public DateTime TokenExpiratedDate { get; private set; }
        public int UserId { get; private set; }
        public User User { get; set; }

        private TokenHistory() { }

        /// <summary>
        /// Método estático para crear una nueva instancia de <see cref="TokenHistory"/>.
        /// </summary>
        public static TokenHistory Create(
            int id, string accessToken, string refreshToken, DateTime tokenCreatedDate, DateTime tokenExpiratedDate, int userId)
        {
            return new TokenHistory
            {
                Id = id,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                TokenCreatedDate = tokenCreatedDate,
                TokenExpiratedDate = tokenExpiratedDate,
                UserId = userId
            };
        }

        /// <summary>
        /// Método de instancia para actualizar los valores del token.
        /// </summary>
        public void Update(string accessToken, string refreshToken, DateTime tokenCreatedDate, DateTime tokenExpiratedDate)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            TokenCreatedDate = tokenCreatedDate;
            TokenExpiratedDate = tokenExpiratedDate;
        }
    }
}
