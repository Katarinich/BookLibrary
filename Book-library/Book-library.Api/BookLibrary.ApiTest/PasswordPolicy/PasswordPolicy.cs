using BookLibrary.Api.Models;
using System.Linq;

namespace BookLibrary.ApiTest
{
    class PasswordPolicy : IPasswordPolicy
    {
        private const int LAST_PASSWORDS_COUNT = 3;
        private const int MIN_PASSWORD_LENGTH = 8;
        private IPasswordHasher _passwordHasher;

        public PasswordPolicy(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }

        public bool SatisfiesPolicy(User user, string newPasswordValue)
        {
            if (newPasswordValue.Length < MIN_PASSWORD_LENGTH)
                return false;

            var orderedPasswords = user.Credentials.Passwords.ToList().OrderByDescending(p => p.ExpirationDate);
            var lastPasswords = orderedPasswords.Take(LAST_PASSWORDS_COUNT);
            var hashedNewPasswordValue = _passwordHasher.GetHash(newPasswordValue);

            if (lastPasswords.ToList().FindAll(p => p.Value == hashedNewPasswordValue).Count > 0)
                return false;

            return true;
        } 
    }
}
