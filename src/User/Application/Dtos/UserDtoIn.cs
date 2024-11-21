using Domain.Interfaces.IDto;

namespace Application.Dtos
{
    public class UserDtoIn : IUserDtoIn
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class UserDtoOut : IUserDtoOut
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
