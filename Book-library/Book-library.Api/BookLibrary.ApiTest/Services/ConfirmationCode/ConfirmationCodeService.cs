using System;
using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Exceptions.CodeExceptions;

namespace BookLibrary.ApiTest.Services.ConfirmationCode
{
    class ConfirmationCodeService : IConfirmationCodeService
    {
        private IConfirmationCodeManager _confirmationCodeManager;
        private ICodeValidationRule _codeValidationRule;

        public ConfirmationCodeService(IConfirmationCodeManager confirmationCodeManager, ICodeValidationRule codeValidationRule)
        {
            _confirmationCodeManager = confirmationCodeManager;
            _codeValidationRule = codeValidationRule;
        }

        public void DeactivateCode(string codeValue)
        {
            var code = _confirmationCodeManager.GetConfirmationCodeByValue(codeValue);
            code.IsActive = false;
            _confirmationCodeManager.UpdateCode();
        }

        public User GetRelatedUser(string codeValue)
        {
            var code = _confirmationCodeManager.GetConfirmationCodeByValue(codeValue);
            return code.Email.User;
        }

        public bool IsCodeValid(string codeValue)
        {
            var code = _confirmationCodeManager.GetConfirmationCodeByValue(codeValue);

            try
            {
                _codeValidationRule.ValidateCode(code);
            }
            catch (Exception ex) when (ex is CodeIsNotExistException || ex is CodeIsNotActiveException || ex is CodeExpirationDateIsUpException)
            {
                return false;
            }

            return true;
        }
    }
}
