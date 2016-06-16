using BookLibrary.Api.Models;

namespace BookLibrary.ApiTest.Services
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

        public void ValidateCode(string codeValue)
        {
            var code = _confirmationCodeManager.GetConfirmationCodeByValue(codeValue);

            _codeValidationRule.ValidateCode(code);
        }

        public void DeactivateCodesByType(User user, ConfirmationCodeType type)
        {
            var codes = _confirmationCodeManager.GetConfirmationCodesByUserId(user.UserId);

            foreach(var code in codes)
            {
                if(code.Type == type)
                    code.IsActive = false;
            }
        }

        public ConfirmationCode GetCodeByValue(string codeValue)
        {
            return _confirmationCodeManager.GetConfirmationCodeByValue(codeValue);
        }
    }
}
