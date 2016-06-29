using BookLibrary.Api.DAL;
using BookLibrary.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace BookLibrary.Api.Managers
{
    public class UserManager : IUserManager
    {
        private BookLibraryContext _context;

        public UserManager(BookLibraryContext context)
        {
            _context = context;
        }

        public User GetUserByLogin(string login)
        {
            User user;

            try
            {
                var loginName = _context.LoginNames
                .Include("Credentials")
                .Include("Credentials.User")
                .Include("Credentials.User.Address")
                .Include("Credentials.User.Credentials")
                .Include("Credentials.User.MobilePhone")
                .Include("Credentials.User.UserRoles")
                .Include("Credentials.User.Emails")
                .Include("Credentials.Passwords")
                .Include("Credentials.Logins")
                .FirstOrDefault(ln => ln.Value == login);

                user = loginName.Credentials.User;
            }
            catch(NullReferenceException)
            {
                user = null;
            }

            return user;
        }

        public User GetUserById(int userId)
        {
            return _context.Users
                .Include("Credentials")
                .Include("Emails")
                .Include("Address")
                .Include("UserRoles")
                .Include("MobilePhone")
                .Include("Credentials.Logins")
                .Include("Credentials.Passwords")
                .FirstOrDefault(u => u.UserId == userId);
        }

        public void UpdateUser()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            return _context.Users
                .Include("Credentials")
                .Include("Emails")
                .Include("Address")
                .Include("UserRoles")
                .Include("MobilePhone")
                .Include("Credentials.Logins")
                .Include("Credentials.Passwords").ToList();
        }
    }
}
