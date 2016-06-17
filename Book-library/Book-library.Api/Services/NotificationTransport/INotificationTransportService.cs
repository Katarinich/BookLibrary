namespace BookLibrary.Api
{
    interface INotificationTransportService
    {
        void SendNotification(string email, string message);
    }
}
