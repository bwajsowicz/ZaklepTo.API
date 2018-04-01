using System;
using System.Security.Cryptography;
using ZaklepTo.Core.Extensions;

namespace ZaklepTo.Infrastructure.Encrypter
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetSalt(string password)
        {
            if (password.Empty())
                throw new ArgumentException("Can't generate salt from empty value.", nameof(password));

            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);

            return Convert.ToBase64String(saltBytes);
        }

        public string GetHash(string password, string salt)
        {
            if (password.Empty())
                throw new ArgumentException("Can't be empty.", nameof(password));
            if (salt.Empty())
                throw new ArgumentException("Can't be empty.", nameof(salt));

            var pbkdf2 = new Rfc2898DeriveBytes(password, GetBytes(salt), DeriveBytesIterationsCount);
            return Convert.ToBase64String(pbkdf2.GetBytes(50));
        }

        private static byte[] GetBytes(string salt)
        {
            var bytes = new byte[salt.Length * sizeof(char)];
            Buffer.BlockCopy(salt.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}