using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Managers;

namespace BookLibrary.ApiTest.Services
{
    class ConfirmationSenderService : IConfirmationSenderService
    {
        private IConfirmationCodeManager _codeManager;
        private IEmailManager _emailManager;

        public ConfirmationSenderService(IConfirmationCodeManager codeManager, IEmailManager emailManager)
        {
            _codeManager = codeManager;
            _emailManager = emailManager;
        }

        public bool SendConfirmation(string emailValue, ConfirmationCodeType type)
        {
            var code = CodeGenerator.Generate(type);

            var email = _emailManager.GetEmailByValue(emailValue);

            if (email == null) return false;

            _codeManager.SaveCode(code, email);

            return true;
        }
    }
}
