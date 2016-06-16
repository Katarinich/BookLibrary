using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Exceptions.CodeExceptions;
using BookLibrary.ApiTest.Managers;


namespace BookLibrary.ApiTest.Services
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
            _userManager.UpdateUser();

            _senderService.SendConfirmation(user.Email, ConfirmationCodeType.EmailChange);
        }

        public void SendConfirmationToNewEmail(string codeValue)
        {
            _confirmationCodeService.ValidateCode(codeValue);

            _confirmationCodeService.DeactivateCode(codeValue);

            var user = _confirmationCodeService.GetRelatedUser(codeValue);
            var newEmailValue = user.Emails.FindLast(x => x.IsConfirmed == false).Value;

            _senderService.SendConfirmation(newEmailValue, ConfirmationCodeType.EmailConfirmation);
        }

        public void ChangeEmail(string codeValue)
        {
            if (_emailConfirmationService.TryAcceptConfirmation(codeValue))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);
                var newEmailValue = _confirmationCodeService.GetCodeByValue(codeValue).Email.Value;

                user.ChangeEmailTo(newEmailValue);

                _confirmationCodeService.DeactivateCode(codeValue);
            }

            else throw new CodeIsNotValidException(codeValue);
        }
    }
}
