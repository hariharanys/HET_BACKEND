using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace HET_BACKEND.Utilities
{
    public class PasswordHash
    {
        private static int _saltSize = 16;
        private static int _keySize = 32; // Smaller salt size, typical for security
        private static int _iterations = 100000;
        private static HashAlgorithmName _algorithm = HashAlgorithmName.SHA512;

        public static (string HashedPassword, string Salt) HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);

            byte[] hashed = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: _iterations,
                hashAlgorithm: _algorithm,
                _keySize);
            return (Convert.ToHexString(hashed), Convert.ToHexString(salt));
        }

        public static bool VerifyPassword(string password, string storedHash, byte[] salt)
        {
            byte[] hash = Convert.FromHexString(storedHash);
            byte[] enteredPasswordHashed = Rfc2898DeriveBytes.Pbkdf2(
                password: password,
                salt: salt,
                iterations: _iterations,
                hashAlgorithm: _algorithm,
                _keySize);

            return CryptographicOperations.FixedTimeEquals(enteredPasswordHashed, hash);
        }
    }
}
