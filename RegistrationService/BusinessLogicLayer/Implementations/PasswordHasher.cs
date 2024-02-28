using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;

namespace BusinessLogicLayer.Implementations
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly int SaltSize = 128 / 8; // 16 bytes
        private readonly int KeySize = 256 / 8; // 32 bytes
        private readonly int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private readonly char Delimeter = ';';
        public string Hash(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, hashAlgorithm, KeySize);
            // delimeter is the char separater
            var result = string.Join(Delimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));

            return result;
        }

        // for the loging 
        public bool Verify(string passwordHash, string inputPassword)
        {
            var elements = passwordHash.Split(Delimeter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);// check the equlity

        }


    }
}
