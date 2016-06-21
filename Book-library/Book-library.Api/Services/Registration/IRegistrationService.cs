using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IRegistrationService
    {
        void RegisterUser(User user);
    }
}
