namespace BookLibrary.ApiTest
{
    interface INotificationTransportService
    {
        void SendNotificate(string email, string message);
    }
}
