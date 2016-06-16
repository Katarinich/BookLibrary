using BookLibrary.ApiTest.Exceptions.CodeExceptions;
using BookLibrary.ApiTest.Services.ConfirmationCode;

namespace BookLibrary.ApiTest.Services
{
    class PasswordRecoveryService : IPasswordRecoveryService
    {
        private IConfirmationCodeService _confirmationCodeService;
        private IPasswordPolicy _passwordPolicy;

        public PasswordRecoveryService(IConfirmationCodeService confirmationCodeService, IPasswordPolicy passwordPolicy)
        {
            _confirmationCodeService = confirmationCodeService;
            _passwordPolicy = passwordPolicy;
        }

        public void RecoverPassword(string newPasswordValue, string codeValue)
        {
            if (_confirmationCodeService.IsCodeValid(codeValue))
            {
                var user = _confirmationCodeService.GetRelatedUser(codeValue);

                if (_passwordPolicy.SatisfiesPolicy(user, newPasswordValue))
                {
                    newPasswordValue = PasswordHasher.GetMd5Hash(newPasswordValue);
                    user.UpdatePassword(newPasswordValue);
                    _confirmationCodeService.DeactivateCode(codeValue);
                }
            }
            else throw new CodeIsNotValidException("Code is not valid");
        }
    }
}
