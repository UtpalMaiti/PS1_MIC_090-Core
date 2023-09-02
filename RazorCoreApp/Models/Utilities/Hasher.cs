using System.Security.Cryptography;

namespace PS1_MIC_090_Core.Models.Utilities
{
    public class Hasher
    {
        private const int SaltSize = 16; //128 / 8, length in bytes
        private const int KeySize = 32; //256 / 8, length in bytes
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithmName = HashAlgorithmName.SHA256;
        private const char SaltDelimeter = ';';
        public static string GenerateHashPassword(string password)
        {
            var salt = GenerateSalt(SaltSize);
            var hash = GenerateHash(password, salt);
            return string.Join(SaltDelimeter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }
        public static bool Validate(string passwordHash, string password)
        {
            var pwdElements = passwordHash.Split(SaltDelimeter);
            var salt = Convert.FromBase64String(pwdElements[0]);
            var hash = Convert.FromBase64String(pwdElements[1]);
            var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
            return CryptographicOperations.FixedTimeEquals(hash, hashInput);
        }

        private static byte[] GenerateSalt(int saltSize)
        {
            return RandomNumberGenerator.GetBytes(saltSize);
        }

        private static byte[] GenerateHash(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithmName, KeySize);
        }
    }
}
