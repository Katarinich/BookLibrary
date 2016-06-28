using Book_library.Api.Exceptions;
using BookLibrary.Api.Managers;
using BookLibrary.Api.Models;
using System.Collections.Generic;
using System.Linq;

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

        public List<User> GetAllUsers()
        {
            return _userManager.GetAllUsers();
        }

        public User UpdateUser(User updatedUser)
        {
            var userToUpdate = _userManager.GetUserByLogin(updatedUser.Emails.First().Value);

            userToUpdate.Address.AddressLine = updatedUser.Address.AddressLine;
            userToUpdate.Address.City = updatedUser.Address.City;
            userToUpdate.Address.Country = updatedUser.Address.Country;
            userToUpdate.Address.Zipcode = updatedUser.Address.Zipcode;
            userToUpdate.Address.State = updatedUser.Address.State;
            userToUpdate.DateOfBirth = updatedUser.DateOfBirth;
            userToUpdate.FirstName = updatedUser.FirstName;
            userToUpdate.LastName = updatedUser.LastName;
            userToUpdate.UserName = updatedUser.UserName;
            userToUpdate.MobilePhone.Value = updatedUser.MobilePhone.Value;
            userToUpdate.Credentials.Logins.First(l => l.Type == LoginType.Username).Value = updatedUser.UserName;
            userToUpdate.Credentials.Logins.First(l => l.Type == LoginType.MobilePhone).Value = updatedUser.MobilePhone.Value;

            if (!_userManager.UpdateUser()) throw new LoginsAreNotUniqException("Username or mobile phone is already taken.");

            return userToUpdate;
        }
    }
}