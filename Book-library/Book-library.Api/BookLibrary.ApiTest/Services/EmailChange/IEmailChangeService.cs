namespace BookLibrary.ApiTest.Services
{
    interface IEmailChangeService
    {
        bool SendToNewEmail(string newEmailValue, string codeValue);
        void ChangeEmail(string newEmailValue, string codeValue);
    }
}