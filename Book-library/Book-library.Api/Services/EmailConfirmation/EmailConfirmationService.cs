using BookLibrary.Api.Exceptions.CodeExceptions;
using BookLibrary.Api.Models;
using System;

namespace BookLibrary.Api.Services
{
    class EmailConfirmationService : IEmailConfirmationService
    {
        private IConfirmationCodeService _confirmationCodeService;

        public EmailConfirmationService(IConfirmationCodeService confirmationCodeService)
        {
            _confirmationCodeService = confirmationCodeService;
        }

        public bool TryAcceptConfirmation(string codeValue, ConfirmationCodeType type)
        {
            try
            {
                _confirmationCodeService.ValidateCode(codeValue, type);
            }
            catch(Exception ex) 
                when (ex is CodeIsNotActiveException || ex is CodeIsNotExistException || ex is CodeExpirationDateIsUpException)
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
