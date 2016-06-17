using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IConfirmationSenderService
    {
        void SendConfirmation(string emailValue, ConfirmationCodeType type);
    }
}
