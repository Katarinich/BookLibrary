using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Managers;

namespace BookLibrary.ApiTest.Services
{
    class AuthentificationService : IAuthentificationService
    {
        private IUserManager _userManager;
        private IPasswordHasher _passwordHasher;

        public AuthentificationService(IUserManager userManager, IPasswordHasher passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public User Authentificate(CredentialsDraft credentialsDraft)
        {
            var user = _userManager.GetUserByLogin(credentialsDraft.login);

            var password = _passwordHasher.GetHash(credentialsDraft.password);

            if (user != null && user.Credentials.Passwords.Find(p => p.IsActive).Value != password)
                user = null;

            return user;
        }
    }
}
