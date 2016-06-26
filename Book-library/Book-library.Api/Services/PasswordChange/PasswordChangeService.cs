using BookLibrary.Api.Models;
using BookLibrary.Api.Exceptions.PasswordExceptions;
using BookLibrary.Api.Managers;
using System;
using System.Linq;

namespace BookLibrary.Api.Services
{
    class PasswordChangeService : IPasswordChangeService
    {
        private IUserManager _userManager;
        private IPasswordPolicy _passwordPolicy;
        private IPasswordHasher _passwordHasher;

        public PasswordChangeService(IUserManager userManager, IPasswordPolicy passwordPolicy, IPasswordHasher passwordHasher)
        {
            _userManager = userManager;
            _passwordPolicy = passwordPolicy;
            _passwordHasher = passwordHasher;
        }

        public void ChangePassword(int userId, string oldPasswordValue, string newPasswordValue)
        {
            var user = _userManager.GetUserById(userId);

            var oldPassword = user.Credentials.Passwords.First(p => p.IsActive);

            if (oldPassword.Value != _passwordHasher.GetHash(oldPasswordValue))
                throw new OldPasswordWrongException("Old password is wrong");

            if (!_passwordPolicy.SatisfiesPolicy(user, newPasswordValue))
                throw new PasswordDoesNotSatisfyPolicyException("New password does not apply password policy.");


            var newPassword = new Password();
            newPassword.Value = _passwordHasher.GetHash(newPasswordValue);
            newPassword.ExpirationDate = DateTimeOffset.Now.AddYears(1);
            newPassword.Credentials = user.Credentials;
            newPassword.IsActive = true;
            user.Credentials.Passwords.Add(newPassword);

            oldPassword.IsActive = false;

            _userManager.UpdateUser();
        }
    }
}
