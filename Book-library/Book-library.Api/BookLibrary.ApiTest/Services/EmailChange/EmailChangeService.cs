using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Exceptions.CodeExceptions;
using BookLibrary.ApiTest.Services.ConfirmationCode;

namespace BookLibrary.ApiTest.Services
{
    class EmailChangeService: IEmailChangeService
    {
        private IConfirmationCodeService _confirmationCodeService;
        private IConfirmationSenderService _senderService;
        private IEmailConfirmationService _emailConfirmationService;

        public EmailChangeService(IConfirmationCodeService confirmationCodeService, 
            IEmailConfirmationService emailConfirmationService, IConfirmationSenderService confirmationSenderService)
        {
            _confirmationCodeService = confirmationCodeService;
            _emailConfirmationService = emailConfirmationService; 
            _senderService = confirmationSenderService;
        }

        public bool SendToNewEmail(string newEmailValue, string codeValue)
        {
            if (_confirmationCodeService.IsCodeValid(codeValue))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);

                var email = new Email();
                email.Value = newEmailValue;
                email.IsConfirmed = false;
                email.IsActive = false;

                user.AddEmail(email);

                _confirmationCodeService.DeactivateCode(codeValue);

                _senderService.SendConfirmation(newEmailValue, ConfirmationCodeType.EmailChange);
            }
            else throw new CodeIsNotValidException("Code is not valid");

            return true;
        }

        public void ChangeEmail(string newEmailValue, string codeValue)
        {
            if (_emailConfirmationService.TryAcceptConfirmation(codeValue, newEmailValue))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);

                user.ChangeEmailTo(newEmailValue);
            }

            else throw new CodeIsNotValidException(codeValue);
        }
    }
}
