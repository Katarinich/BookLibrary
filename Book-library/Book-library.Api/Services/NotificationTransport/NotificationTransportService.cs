using System.Net;
using System.Net.Mail;

namespace BookLibrary.Api.Services.NotificationTransport
{
    class NotificationTransportService : INotificationTransportService
    {
        private readonly string emailFrom = "robot@book-library.com";
        private readonly string server = "localhost";
        private readonly int port = 25;

        public void SendNotification(string email, string message)
        {
            MailMessage emailMessage = new MailMessage(
               emailFrom,
               email,
               "Test notification",
               message);

            SmtpClient client = new SmtpClient(server, port);
            client.Credentials = CredentialCache.DefaultNetworkCredentials;

            client.Send(emailMessage);
        }
    }
}
