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

        /// <summary>
        /// Constructor que inicializa un nuevo usuario con el nombre, apellido, correo electrónico y contraseña.
        /// </summary>
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

        /// <summary>
        /// Actualiza parcialmente los campos del usuario.
        /// Solo se actualizan los campos que no sean nulos o vacíos y que hayan cambiado.
        /// </summary>
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

        /// <summary>
        /// Verifica si la contraseña proporcionada coincide con la contraseña cifrada almacenada.
        /// </summary>
        /// <param name="password">La contraseña en texto plano a verificar.</param>
        /// <returns>True si la contraseña es válida, de lo contrario false.</returns>
        public bool VerifyPassword(string password)
        {
            var hashToVerify = Encryption.EncryptPassword(password, Salt);
            return Hash == hashToVerify;
        }

        /// <summary>
        /// Actualiza el correo electrónico del usuario.
        /// </summary>
        /// <param name="newEmail">El nuevo correo electrónico.</param>
        public void UpdateEmail(string newEmail)
        {
            Email = newEmail;
            UpdatedDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Desactiva al usuario, estableciendo el estado de "Active" como false.
        /// </summary>
        public void Deactivate()
        {
            Active = false;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
