using BookLibrary.Api.Models;

namespace BookLibrary.Api
{
    interface ICodeValidationRule
    {
        void ValidateCode(ConfirmationCode code, ConfirmationCodeType type);
    }
}