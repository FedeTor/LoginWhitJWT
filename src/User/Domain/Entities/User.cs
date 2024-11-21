using Domain.Security;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Salt { get; private set; }
        public string Hash { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? UpdatedDate { get; private set; }
        public bool Active { get; private set; }

        public User() { }

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CreatedDate = DateTime.UtcNow;
            Active = true;
            Salt = SaltCreating.GenerateSalt();
            Hash = Encryption.EncryptPassword(password, Salt);
        }

        public void UpdatePartial(string firstName, string lastName, string email, string password)
        {
            if (!string.IsNullOrWhiteSpace(firstName) && firstName != FirstName)
                FirstName = firstName;

            if (!string.IsNullOrWhiteSpace(lastName) && lastName != LastName)
                LastName = lastName;

            if (!string.IsNullOrWhiteSpace(email) && email != Email)
                UpdateEmail(email);

            if (!string.IsNullOrWhiteSpace(password))
            {
                if (!VerifyPassword(password))
                {
                    Salt = SaltCreating.GenerateSalt();
                    Hash = Encryption.EncryptPassword(password, Salt);
                    UpdatedDate = DateTime.UtcNow;
                }
            }

            if (!string.IsNullOrWhiteSpace(firstName) || !string.IsNullOrWhiteSpace(lastName) ||
                !string.IsNullOrWhiteSpace(email))
            {
                UpdatedDate = DateTime.UtcNow;
            }
        }

        public bool VerifyPassword(string password)
        {
            var hashToVerify = Encryption.EncryptPassword(password, Salt);
            return Hash == hashToVerify;
        }

        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            UpdatedDate = DateTime.UtcNow;
        }

        public void Deactivate()
        {
            Active = false;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
