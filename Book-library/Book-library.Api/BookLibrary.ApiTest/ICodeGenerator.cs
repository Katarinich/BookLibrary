using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest
{
    interface ICodeGenerator
    {
        ConfirmationCode GenerateCode(ConfirmationCodeType type);
    }
}
