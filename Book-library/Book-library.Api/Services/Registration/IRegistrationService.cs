using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IRegistrationService
    {
        void RegisterUser(User user);
    }
}
