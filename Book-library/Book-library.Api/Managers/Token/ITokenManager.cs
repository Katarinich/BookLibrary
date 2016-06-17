using BookLibrary.Api.Models;

namespace BookLibrary.Api.Managers
{
    public interface ITokenManager
    {
        void AddToken(Token token);
        Token GetTokenByValue(string tokenValue);
    }
}
