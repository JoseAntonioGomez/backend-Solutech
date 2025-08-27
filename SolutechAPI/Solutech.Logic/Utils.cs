using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Solutech.Logic
{
    public static class Utils
    {
        const int Iter = 100_000, SaltSize = 16, KeySize = 32;

        public static string HashPass(string pass)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            using var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, Iter, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);
            return $"{Iter}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        public static bool VerifyPass(string pass, string token)
        {
            var parts = token.Split('.', 3);
            int iter = int.Parse(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(pass, salt, iter, HashAlgorithmName.SHA256);
            var keyCheck = pbkdf2.GetBytes(key.Length);
            return CryptographicOperations.FixedTimeEquals(key, keyCheck);
        }
    }
}
