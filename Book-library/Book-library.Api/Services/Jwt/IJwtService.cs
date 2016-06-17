using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IJwtService
    {
        Token CreateToken(User user);
    }
}
