using System;
using System.Security.Cryptography;

namespace TravisRFrench.UI.MVVM.Samples.Common
{
    public static class PasswordUtilities
    {
        // OWASP commonly recommends PBKDF2 with a high iteration count; tune as needed for your target devices.
        private const int SaltSize = 16;        // 128-bit
        private const int HashSize = 32;        // 256-bit
        private const int Iterations = 210000; // reasonable 2024+ baseline; adjust per perf budget

        /// <summary>
        /// Creates a password-safe hash using PBKDF2 (SHA-256) with a random salt.
        /// Output format: pbkdf2-sha256$<iterations>$<base64(salt)>$<base64(hash)>
        /// </summary>
        public static string HashPasswordText(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);
        
            var hash = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256)
                .GetBytes(HashSize);

            return $"pbkdf2-sha256${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verifies a password against a stored hash (constant-time compare).
        /// Supports the format produced by HashPassword().
        /// </summary>
        public static bool VerifyPassword(string password, string stored)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
        
            if (string.IsNullOrWhiteSpace(stored))
            {
                return false;
            }

            // Expected: pbkdf2-sha256$iters$saltB64$hashB64
            var parts = stored.Split('$');
            if (parts.Length != 4)
            {
                return false;
            }
            if (!string.Equals(parts[0], "pbkdf2-sha256", StringComparison.Ordinal))
            {
                return false;
            }

            if (!int.TryParse(parts[1], out var iterations) || iterations <= 0)
            {
                return false;
            }

            byte[] salt;
            byte[] expectedHash;
            try
            {
                salt = Convert.FromBase64String(parts[2]);
                expectedHash = Convert.FromBase64String(parts[3]);
            }
            catch (FormatException)
            {
                return false;
            }

            var actualHash = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256)
                .GetBytes(expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
    }
}
