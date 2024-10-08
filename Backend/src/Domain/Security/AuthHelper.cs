using DotNetEnv;
using Konscious.Security.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Security
{
    public class AuthHelper : IAuthHelper
    {
        public AuthHelper()
        {
            Env.Load();
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var pepper = Environment.GetEnvironmentVariable("JWT_PEPPER")
                ?? throw new NullReferenceException("Pepper cannot be null");

            // Generate salt (16 bytes)
            passwordSalt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(passwordSalt);
            }

            // Include the pepper
            string passwordWithPepper = password + pepper;

            // Argon2id hasher
            using (var argon2 = new Argon2id(Encoding.UTF8.GetBytes(passwordWithPepper)))
            {
                argon2.Salt = passwordSalt;

                // Hashing
                argon2.DegreeOfParallelism = 8;
                argon2.Iterations = 4;
                argon2.MemorySize = 65536;
                passwordHash = argon2.GetBytes(64);
            }
        }

        public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            var pepper = Environment.GetEnvironmentVariable("JWT_PEPPER")
                ?? throw new NullReferenceException("Pepper cannot be null");
            // Include the pepper
            var passwordWithPepper = password + pepper;

            // Create an Argon2id verifier
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(passwordWithPepper));
            argon2.Salt = storedSalt;
            argon2.DegreeOfParallelism = 8;
            argon2.Iterations = 4;
            argon2.MemorySize = 65536;

            var inputPasswordHash = argon2.GetBytes(64);

            return storedHash.SequenceEqual(inputPasswordHash);
        }
    }
}
