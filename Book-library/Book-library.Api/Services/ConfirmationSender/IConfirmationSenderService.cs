using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    public interface IConfirmationSenderService
    {
        void SendConfirmation(string emailValue, ConfirmationCodeType type);
    }
}
