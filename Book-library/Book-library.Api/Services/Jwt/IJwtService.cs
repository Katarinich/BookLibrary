using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IJwtService
    {
        string CreateToken(int userId);

        bool ValidateToken(string tokenValue);

        int GetUserIdFromToken(string tokenValue);
    }
}
