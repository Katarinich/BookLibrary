using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IUserService
    {
        User GetUserByLogins(string[] logins);
    }
}
