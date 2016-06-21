using BookLibrary.Api.Models;
using System.Collections.Generic;

namespace BookLibrary.Api.Managers
{
    public interface IUserManager
    {
        User GetUserByLogin(string login);

        User GetUserById(int userId);

        void AddUser(User user);

        void UpdateUser();

        List<User> GetAllUsers();
    }
}
