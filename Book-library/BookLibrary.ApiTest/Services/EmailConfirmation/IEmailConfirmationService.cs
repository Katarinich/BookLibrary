namespace BookLibrary.Api.Services
{
    interface IEmailConfirmationService
    {
        bool TryAcceptConfirmation(string codeValue);
    }
}