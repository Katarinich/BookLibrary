using BookLibrary.Api.Models;
using BookLibrary.Api.Managers;
using System.Data.Entity.Infrastructure;

namespace BookLibrary.Api.Services
{
    class RegistrationService : IRegistrationService
    {
        private IUserManager _userManager;

        public RegistrationService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public void RegisterUser(User user)
        {
            try
            {
                _userManager.AddUser(user);
            }
            catch(DbUpdateException ex)
            {
                throw ex;
            }
        }
    }
}
