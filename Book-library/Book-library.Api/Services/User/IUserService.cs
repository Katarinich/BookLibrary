using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IUserService
    {
        User GetUserByLogins(string[] logins);
    }
}
