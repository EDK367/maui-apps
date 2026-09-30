using System;
using System.Collections.Generic;
using System.Text;

namespace Notificaciones.Interfaces
{
    public interface INotificationManagerService
    {
        event EventHandler NotificationReceived;
        void SendNotification(string title, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);
    }
}
