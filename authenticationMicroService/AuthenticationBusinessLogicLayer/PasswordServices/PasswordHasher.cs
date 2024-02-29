using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationBusinessLogicLayer.PasswordServices
{
    public class PasswordHasher : IPasswordHasher
    {
        //private readonly int SaltSize = 128 / 8; // 16 bytes
        private readonly int KeySize = 256 / 8; // 32 bytes
        private readonly int Iterations = 10000;
        private static readonly HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA256;
        private readonly char Delimeter = ';';


        // for the loging 
        public bool Verify(string DbPassword, string inputPassword)
        {
            var elements = DbPassword.Split(Delimeter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, Iterations, hashAlgorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);// check the equlity

        }
    }
}
