namespace BookLibrary.Api.Services
{
    interface IEmailChangeService
    {
        void SendConfirmationToNewEmail(string codeValue);

        void ChangeEmail(string codeValue);

        void InitiateChangeEmailProcess(int userId, string newEmailValue);
    }
}