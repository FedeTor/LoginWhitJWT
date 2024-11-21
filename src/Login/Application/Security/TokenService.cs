using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Security
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(string idUser)
        {
            var key = _configuration.GetSection("JwtSettings")["key"];
            var keyBytes = Encoding.ASCII.GetBytes(key!);
            var accessTokenExpire = int.Parse(_configuration.GetSection("JwtSettings")["AccessTokenExpire"]);

            var claims = new ClaimsIdentity(new[] {
            new Claim("IdUser", idUser)
        });

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
            );
            var horaLocal = DateTime.UtcNow;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = horaLocal.ToUniversalTime().AddMinutes(accessTokenExpire),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(tokenConfig);
        }

        public string GenerateRefreshToken()
        {
            var byteArray = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
                return Convert.ToBase64String(byteArray);
            }
        }
    }
}
