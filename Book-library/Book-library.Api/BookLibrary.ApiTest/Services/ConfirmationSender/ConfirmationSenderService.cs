using BookLibrary.Api.Models;
using BookLibrary.ApiTest.Managers;

namespace BookLibrary.ApiTest.Services
{
    class ConfirmationSenderService : IConfirmationSenderService
    {
        private IConfirmationCodeManager _codeManager;
        private IEmailManager _emailManager;
        private ICodeGenerator _codeGenerator;

        public ConfirmationSenderService(IConfirmationCodeManager codeManager, IEmailManager emailManager, 
            ICodeGenerator codeGenerator)
        {
            _codeManager = codeManager;
            _emailManager = emailManager;
            _codeGenerator = codeGenerator;
        }

        public void SendConfirmation(string emailValue, ConfirmationCodeType type)
        {
            var code = _codeGenerator.GenerateCode(type);

            var email = _emailManager.GetEmailByValue(emailValue);

            if (email == null) throw new EmailNotFoundException(emailValue);

            _codeManager.SaveCode(code, email);
        }
    }
}
