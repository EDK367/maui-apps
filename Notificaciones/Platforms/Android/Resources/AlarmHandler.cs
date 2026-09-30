using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace Notificaciones.Platforms.Android.Resources
{
    [BroadcastReceiver(Enabled = true, Label ="Transmisor receptor de notificaciones")]
    public class AlarmHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent?.Extras != null)
            {
                string title = intent.GetStringExtra(NotificationManagerService.TitleKey);
                string message = intent.GetStringExtra(NotificationManagerService.MessageKey);

                NotificationManagerService manager = 
                    NotificationManagerService.Instance ?? 
                    new NotificationManagerService();
                manager.Show(title, message);
            }
        }
    }
}
