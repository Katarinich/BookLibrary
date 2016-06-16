using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services
{
    interface IConfirmationSenderService
    {
        void SendConfirmation(string emailValue, ConfirmationCodeType type);
    }
}
