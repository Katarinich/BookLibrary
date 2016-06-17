using BookLibrary.Api.Models;

namespace BookLibrary.Api
{
    interface ICodeGenerator
    {
        ConfirmationCode GenerateCode(ConfirmationCodeType type);
    }
}
