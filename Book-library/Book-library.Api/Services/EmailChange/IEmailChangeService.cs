namespace BookLibrary.Api.Services
{
    public interface IEmailChangeService
    {
        bool TrySendConfirmationToNewEmail(string codeValue);

        string ChangeEmail(string codeValue);

        void InitiateChangeEmailProcess(int userId, string newEmailValue);
    }
}