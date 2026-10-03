using System;
using System.Collections.Generic;
using System.Text;

namespace parcial2Progra.Interfaces
{
    public interface INotificationManagerService
    {
        event EventHandler NotificationReceived;
        void SendNotification(string title, string message, DateTime? notifyTime = null);
        void ReceiveNotification(string title, string message);
    }
}
