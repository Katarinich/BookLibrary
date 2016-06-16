using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Managers;

namespace BookLibrary.ApiTest.Services
{
    class LoginService : ILoginService
    {
        private IUserManager _userManager;

        public LoginService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public User Authentification(CredentialsDraft credentialsDraft)
        {
            var user = _userManager.GetUserByLogin(credentialsDraft.login);

            var password = PasswordHasher.GetMd5Hash(credentialsDraft.password);

            if (user != null && user.Credentials.Passwords.Find(p => p.IsActive).Value != password)
                user = null;

            return user;
        }
    }
}
