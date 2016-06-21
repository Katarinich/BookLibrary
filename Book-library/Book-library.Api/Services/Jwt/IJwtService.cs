using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IJwtService
    {
        Token CreateToken(User user);
    }
}
