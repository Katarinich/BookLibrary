using BookLibrary.Api.Exceptions;
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

        public void CheckIfUserExistByLogin(string[] logins)
        {
            User user = null;

            foreach(var login in logins)
            {
                user = _userManager.GetUserByLogin(login);

                if (user != null)
                {
                    var loginType = user.Credentials.Logins.First(l => l.Value == login).Type;
                    if (loginType == LoginType.Email)
                        throw new UserAlreadyExistException("User with this email is already exist");
                    if (loginType == LoginType.MobilePhone)
                        throw new UserAlreadyExistException("User with this mobile phone is already exist");
                    if(loginType == LoginType.Username)
                        throw new UserAlreadyExistException("User with this username is already exist");
                }
            }
        }

        public List<User> GetAllUsers()
        {
            return _userManager.GetAllUsers();
        }

        public User UpdateUser(User userToUpdate, User updatedUser)
        {
            userToUpdate.Address.AddressLine = updatedUser.Address.AddressLine;
            userToUpdate.Address.City = updatedUser.Address.City;
            userToUpdate.Address.Country = updatedUser.Address.Country;
            userToUpdate.Address.Zipcode = updatedUser.Address.Zipcode;
            userToUpdate.Address.State = updatedUser.Address.State;
            userToUpdate.DateOfBirth = updatedUser.DateOfBirth;
            userToUpdate.FirstName = updatedUser.FirstName;
            userToUpdate.LastName = updatedUser.LastName;

            if (userToUpdate.UserName != updatedUser.UserName)
            {
                userToUpdate.UserName = updatedUser.UserName;
                userToUpdate.Credentials.Logins.First(l => l.Type == LoginType.Username).Value = updatedUser.UserName;
            }

            if (userToUpdate.MobilePhone.Value != updatedUser.MobilePhone.Value)
            {
                userToUpdate.MobilePhone.Value = updatedUser.MobilePhone.Value;
                userToUpdate.Credentials.Logins.First(l => l.Type == LoginType.MobilePhone).Value = updatedUser.MobilePhone.Value;
            }

            _userManager.UpdateUser();
            return userToUpdate;
        }

        public User GetUserByLogin(string login)
        {
            return _userManager.GetUserByLogin(login);
        }
    }
}