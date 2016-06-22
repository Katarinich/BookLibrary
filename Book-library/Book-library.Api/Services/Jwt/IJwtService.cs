using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IJwtService
    {
        Token CreateToken(User user);

        bool ValidateToken(string tokenValue);

        void DeactivateToken(string tokenValue);
    }
}
