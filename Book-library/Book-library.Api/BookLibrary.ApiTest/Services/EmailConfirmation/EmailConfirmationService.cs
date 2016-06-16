using BookLibrary.ApiTest.Exceptions.CodeExceptions;
using BookLibrary.ApiTest.Services.ConfirmationCode;

namespace BookLibrary.ApiTest.Services
{
    class EmailConfirmationService : IEmailConfirmationService
    {
        private IConfirmationCodeService _confirmationCodeService;

        public EmailConfirmationService(IConfirmationCodeService confirmationCodeService)
        {
            _confirmationCodeService = confirmationCodeService;
        }

        public bool TryAcceptConfirmation(string codeValue, string emailValue)
        {
            if (_confirmationCodeService.IsCodeValid(codeValue))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);

                user.Emails.Find(e => e.Value == emailValue).IsConfirmed = true;

                _confirmationCodeService.DeactivateCode(codeValue);
            }
            else return false;

            return true;
        }
    }
}
