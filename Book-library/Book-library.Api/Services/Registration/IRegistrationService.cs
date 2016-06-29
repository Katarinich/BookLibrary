using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IRegistrationService
    {
        bool TryRegisterUser(User user);
    }
}
