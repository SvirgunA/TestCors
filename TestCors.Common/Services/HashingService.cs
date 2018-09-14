using System;
using System.Security.Cryptography;

namespace TestCors.Common.Services
{
    public static class HashingService
    {
        private const int PBKDF2_ITERATIONS_COUNT = 1000; // default for Rfc2898DeriveBytes
        private const int PBKDF2_SUBKEY_LENGTH = 256 / 8; // 256 bits
        private const int SALT_SIZE = 128 / 8; // 128 bits

        public static string Hash(this string data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            byte[] salt;
            byte[] subkey;
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(data, SALT_SIZE, PBKDF2_ITERATIONS_COUNT))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2_SUBKEY_LENGTH);
            }

            byte[] outputBytes = new byte[1 + SALT_SIZE + PBKDF2_SUBKEY_LENGTH];

            Buffer.BlockCopy(salt, 0, outputBytes, 1, SALT_SIZE);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SALT_SIZE, PBKDF2_SUBKEY_LENGTH);

            return Convert.ToBase64String(outputBytes);
        }

        public static bool VerifyHashed(this string hashed, string data)
        {
            if (hashed == null)
            {
                return false;
            }

            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            byte[] hashedPasswordBytes = Convert.FromBase64String(hashed);


            if (hashedPasswordBytes.Length != (1 + SALT_SIZE + PBKDF2_SUBKEY_LENGTH) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            byte[] salt = new byte[SALT_SIZE];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SALT_SIZE);
            byte[] storedSubkey = new byte[PBKDF2_SUBKEY_LENGTH];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SALT_SIZE, storedSubkey, 0, PBKDF2_SUBKEY_LENGTH);

            byte[] generatedSubkey;
            using (Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(data, salt, PBKDF2_ITERATIONS_COUNT))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2_SUBKEY_LENGTH);
            }

            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            bool areSame = true;
            for (int i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
    }
}
