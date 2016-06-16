using BookLibrary.ApiTest.Exceptions.CodeExceptions;
using System;

namespace BookLibrary.ApiTest.Services
{
    class EmailConfirmationService : IEmailConfirmationService
    {
        private IConfirmationCodeService _confirmationCodeService;

        public EmailConfirmationService(IConfirmationCodeService confirmationCodeService)
        {
            _confirmationCodeService = confirmationCodeService;
        }

        public bool TryAcceptConfirmation(string codeValue)
        {
            try
            {
                _confirmationCodeService.ValidateCode(codeValue);
            }
            catch(Exception ex) 
                when (ex is CodeIsNotActiveException || ex is CodeIsNotExistException || ex is CodeIsNotValidException)
            {
                return false;
            }
            var user = _confirmationCodeService.GetRelatedUser(codeValue);
            var emailValue = _confirmationCodeService.GetCodeByValue(codeValue).Email.Value;

            user.Emails.Find(e => e.Value == emailValue).IsConfirmed = true;

            _confirmationCodeService.DeactivateCode(codeValue);

            return true;
        }
    }
}
