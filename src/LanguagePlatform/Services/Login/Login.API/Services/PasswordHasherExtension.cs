using Login.API.Helpers.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Login.API.Services
{
    public class PasswordHasherExtension : IPasswordHasherExtension
    {
        public string GenerateSalt()
        {
            byte[] saltBytes = Guid.NewGuid().ToByteArray();
            return Convert.ToBase64String(saltBytes);
        }
    }
}
