using System;
using System.Security.Cryptography;

namespace FixFlow.Services
{
    public static class PasswordHasher
    {
        private const string Prefix = "PBKDF2";
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 100000;

        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be empty.");

            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            byte[] key = Derive(password, salt, Iterations, KeySize);
            return Prefix + "$" + Iterations + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(key);
        }

        public static bool Verify(string password, string storedHash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(storedHash))
                return false;

            string[] parts = storedHash.Split('$');
            if (parts.Length != 4 || parts[0] != Prefix)
                return false;

            int iterations;
            if (!int.TryParse(parts[1], out iterations) || iterations <= 0)
                return false;

            byte[] salt;
            byte[] expected;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expected = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            if (salt.Length < 8 || expected.Length == 0)
                return false;

            byte[] actual = Derive(password, salt, iterations, expected.Length);

            int difference = 0;
            for (int i = 0; i < expected.Length; i++)
                difference |= actual[i] ^ expected[i];
            return difference == 0;
        }

        private static byte[] Derive(string password, byte[] salt, int iterations, int length)
        {
            using (Rfc2898DeriveBytes kdf = new Rfc2898DeriveBytes(
                password, salt, iterations, HashAlgorithmName.SHA256))
            {
                return kdf.GetBytes(length);
            }
        }
    }
}
