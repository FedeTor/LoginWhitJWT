using System.Security.Cryptography;
using System.Text;

namespace Application.Security
{
    public static class EncryptionService
    {
        public static string EncryptPassword(string password, string salt)
        {
            byte[] byteSalt = Convert.FromBase64String(salt);

            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            byte[] passAndSalt = new byte[passBytes.Length + byteSalt.Length];
            Array.Copy(passBytes, passAndSalt, passBytes.Length);
            Array.Copy(byteSalt, 0, passAndSalt, passBytes.Length, byteSalt.Length);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passAndSalt);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
