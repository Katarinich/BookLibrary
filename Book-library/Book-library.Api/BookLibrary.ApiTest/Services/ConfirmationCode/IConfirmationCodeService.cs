using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services.ConfirmationCode
{
    interface IConfirmationCodeService
    {
        bool IsCodeValid(string codeValue);

        User GetRelatedUser(string codeValue);

        void DeactivateCode(string codeValue);
    }
}