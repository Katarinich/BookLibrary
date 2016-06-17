using BookLibrary.Api.Models;

namespace BookLibrary.Api.Managers
{
    public interface IUserManager
    {
        User GetUserByLogin(string login);

        User GetUserById(int userId);

        void AddUser(User user);

        void UpdateUser();
    }
}
