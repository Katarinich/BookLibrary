using BookLibrary.Api.Models;
using BookLibrary.Api.Exceptions.CodeExceptions;
using BookLibrary.Api.Managers;
using BookLibrary.Api.Exceptions;
using System;
using System.Data.Entity.Infrastructure;

namespace BookLibrary.Api.Services
{
    class EmailChangeService: IEmailChangeService
    {
        private IConfirmationCodeService _confirmationCodeService;
        private IConfirmationSenderService _senderService;
        private IUserManager _userManager;
        private IEmailConfirmationService _emailConfirmationService;

        public EmailChangeService(IConfirmationCodeService confirmationCodeService, IUserManager userManager,
            IEmailConfirmationService emailConfirmationService, IConfirmationSenderService confirmationSenderService)
        {
            _userManager = userManager;
            _emailConfirmationService = emailConfirmationService;
            _confirmationCodeService = confirmationCodeService;
            _senderService = confirmationSenderService;
        }

        public void InitiateChangeEmailProcess(int userId, string newEmailValue)
        {
            var user = _userManager.GetUserById(userId);
            _confirmationCodeService.DeactivateCodesByType(user, ConfirmationCodeType.EmailChange);

            var newEmail = new Email();
            newEmail.IsActive = false;
            newEmail.IsConfirmed = false;
            newEmail.User = user;
            newEmail.Value = newEmailValue;

            user.AddEmail(newEmail);

            try
            {
                _userManager.UpdateUser();
            }
            catch (DbUpdateException)
            {
                throw new EmailIsAlredyTakenException("Email is alredy taken!");
            }

            _senderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailChange);
        }

        public bool TrySendConfirmationToNewEmail(string codeValue)
        {
            try
            {
                _confirmationCodeService.ValidateCode(codeValue, ConfirmationCodeType.EmailChange);
            }
            catch (Exception ex)
                when (ex is CodeIsNotActiveException || ex is CodeIsNotExistException || ex is CodeExpirationDateIsUpException)
            {
                return false;
            }

            _confirmationCodeService.DeactivateCode(codeValue);

            var user = _confirmationCodeService.GetRelatedUser(codeValue);
            var newEmailValue = user.Emails.FindLast(x => x.IsConfirmed == false).Value;

            _senderService.SendConfirmation(newEmailValue, ConfirmationCodeType.EmailChangeConfirmation);

            return true;
        }

        public string ChangeEmail(string codeValue)
        {
            string newEmailValue;

            if (_emailConfirmationService.TryAcceptConfirmation(codeValue, ConfirmationCodeType.EmailChangeConfirmation))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);
                newEmailValue = _confirmationCodeService.GetCodeByValue(codeValue).Email.Value;

                user.ChangeEmailTo(newEmailValue);

                _confirmationCodeService.DeactivateCode(codeValue);
            }

            else throw new CodeIsNotValidException(codeValue);

            return newEmailValue;
        }
    }
}
