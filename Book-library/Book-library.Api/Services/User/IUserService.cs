using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.Api.Services
{
    public interface IUserService
    {
        void CheckIfUserExistByLogin(string[] logins);

        List<User> GetAllUsers();

        User UpdateUser(User userToUpdate, User updatedUser);

        User GetUserByLogin(string login);
    }
}
