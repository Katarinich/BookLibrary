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
        private readonly string clientUrl = "http://localhost:3000";

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

            string linkType;

            _codeManager.SaveCode(code, email);

            if (type == ConfirmationCodeType.EmailChange)
            {
                linkType = "/email-change/";
            }
            else if (type == ConfirmationCodeType.EmailChangeConfirmation)
            {
                linkType = "/email-change-confirm/";
            }
            else if (type == ConfirmationCodeType.EmailConfirmation)
            {
                linkType = "/email-confirm/";
            }
            else
            {
                linkType = "/password-recover/";
            }


            var linkWithCode = clientUrl + linkType + code.Value;

            _notificationTransportService.SendNotification(emailValue, linkWithCode);
        }
    }
}
