using BookLibrary.Api.Models;
using BookLibrary.Api.Managers;

namespace BookLibrary.Api.Services
{
    class ConfirmationSenderService : IConfirmationSenderService
    {
        private IConfirmationCodeManager _codeManager;
        private IEmailManager _emailManager;
        private ICodeGenerator _codeGenerator;
        private INotificationTransportService _notificationTransportService;

        public ConfirmationSenderService(IConfirmationCodeManager codeManager, IEmailManager emailManager, 
            ICodeGenerator codeGenerator, INotificationTransportService notificationTransportService)
        {
            _codeManager = codeManager;
            _emailManager = emailManager;
            _codeGenerator = codeGenerator;
            _notificationTransportService = notificationTransportService;
        }

        public void SendConfirmation(string emailValue, ConfirmationCodeType type)
        {
            var code = _codeGenerator.GenerateCode(type);

            var email = _emailManager.GetEmailByValue(emailValue);

            if (email == null) throw new EmailNotFoundException(emailValue);

            _codeManager.SaveCode(code, email);

            _notificationTransportService.SendNotification(emailValue, code.Value);
        }
    }
}
