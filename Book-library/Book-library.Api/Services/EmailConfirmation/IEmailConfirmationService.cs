using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IEmailConfirmationService
    {
        bool TryAcceptConfirmation(string codeValue, ConfirmationCodeType type);
    }
}