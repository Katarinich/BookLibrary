using BookLibrary.Api.Models;

namespace BookLibrary.Api.Services
{
    interface IConfirmationCodeService
    {
        void ValidateCode(string codeValue, ConfirmationCodeType type);

        User GetRelatedUser(string codeValue);

        void DeactivateCode(string codeValue);

        void DeactivateCodesByType(User user, ConfirmationCodeType type);

        ConfirmationCode GetCodeByValue(string codeValue);
    }
}