using BookLibrary.Api.Managers;
using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public class UserService : IUserService
    {
        private IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public User GetUserByLogins(string[] logins)
        {
            User user = null;

            foreach(var login in logins)
            {
                user = _userManager.GetUserByLogin(login);

                if (user != null) return user;
            }

            return user;
        }
    }
}