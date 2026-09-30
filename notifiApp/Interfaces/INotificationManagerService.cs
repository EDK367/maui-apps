namespace notifiApp.interfaces
{
    public interface INotificationManagerService
    {
        event EventHandler NotificationReceived;

        void SendNotification(string title, string message, DateTime? notificationTime = null);
        void ReceiveNotification(string title, string message);
    }
}
