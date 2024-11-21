namespace Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public string? Email { get; private set; }
        public string? Salt { get; private set; }
        public string? Hash { get; private set; }
        public DateTime? CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool? Active { get; private set; }
    }
}
