using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest
{
    public interface IConfirmationCodeManager
    {
        void SaveCode(ConfirmationCode code, Email email);

        ConfirmationCode GetConfirmationCodeByValue(string codeValue);

        void UpdateCode();
    }
}