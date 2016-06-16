using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services
{
    interface IConfirmationSenderService
    {
        bool SendConfirmation(string emailValue, ConfirmationCodeType type);
    }
}
