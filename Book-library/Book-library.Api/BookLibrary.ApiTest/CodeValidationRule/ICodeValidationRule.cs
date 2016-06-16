using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest
{
    interface ICodeValidationRule
    {
        void ValidateCode(ConfirmationCode code);
    }
}