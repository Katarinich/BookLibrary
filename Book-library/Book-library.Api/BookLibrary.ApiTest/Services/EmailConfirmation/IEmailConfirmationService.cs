namespace BookLibrary.ApiTest.Services
{
    interface IEmailConfirmationService
    {
        bool TryAcceptConfirmation(string codeValue, string emailValue);
    }
}