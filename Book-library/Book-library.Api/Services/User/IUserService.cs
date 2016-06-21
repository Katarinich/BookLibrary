using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.Api.Services
{
    public interface IUserService
    {
        User GetUserByLogins(string[] logins);

        List<User> GetAllUsers();
    }
}
