using Domain.Interfaces.IDto;

namespace Application.Dtos
{
    public class LoginDto : ILoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
