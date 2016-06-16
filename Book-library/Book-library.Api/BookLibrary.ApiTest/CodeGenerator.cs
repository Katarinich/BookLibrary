using BookLibrary.Api.Models;
using System;

namespace BookLibrary.ApiTest
{
    class CodeGenerator : ICodeGenerator
    {
        private const int CODE_LENGTH = 8;
        public ConfirmationCode GenerateCode(ConfirmationCodeType type)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var generator = new Generator();
            var value = generator.GenerateString(chars);

            var experationDate = DateTime.Now.AddHours(12);

            var confirmationCode = new ConfirmationCode();
            confirmationCode.ExpirationDate = experationDate;
            confirmationCode.Value = value;
            confirmationCode.IsActive = true;
            confirmationCode.Type = type;

            return confirmationCode;
        }
    }
}
