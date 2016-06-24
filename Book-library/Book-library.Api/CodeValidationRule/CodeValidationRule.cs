using BookLibrary.Api.Models;
using BookLibrary.Api.Exceptions.CodeExceptions;
using System;

namespace BookLibrary.Api
{
    class CodeValidationRule : ICodeValidationRule
    {
        public void ValidateCode(ConfirmationCode code, ConfirmationCodeType type)
        {
            if (code == null)
                throw new CodeIsNotExistException("Code is not exist");

            if (!code.IsActive)
                throw new CodeIsNotActiveException("Code is not active.");

            if (DateTime.Now.Subtract(code.ExpirationDate.DateTime).Seconds > 0)
                throw new CodeExpirationDateIsUpException("Code's Experation Date is up.");

            if (type != code.Type)
                throw new CodeHasWrongTypeException(code.Value);
        }
    }
}
