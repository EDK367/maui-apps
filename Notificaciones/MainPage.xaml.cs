using Notificaciones.Interfaces;
#if ANDROID
using Notificaciones.Platforms.Android;
#endif

namespace Notificaciones
{
    public partial class MainPage : ContentPage
    {
        INotificationManagerService? notificationManager;
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            notificationManager = IPlatformApplication.Current?.Services?.GetService<INotificationManagerService>();
        }

        public MainPage(INotificationManagerService manager) : this()
        {
            if (manager != null)
            {
                this.notificationManager = manager;
            }
        }

#if ANDROID
        protected override async void OnAppearing()    
        {
            base.OnAppearing();
            PermissionStatus status = await Permissions.RequestAsync<NotificationPermission>();
        }
#endif

        private void OnCounterClicked(object? sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            string title = $"Notificación local #{count}";
            string message = $"¡Has recibido {count} notificaciones!";

            if (notificationManager == null)
            {
                notificationManager = IPlatformApplication.Current?.Services?.GetService<INotificationManagerService>();
            }

            notificationManager?.SendNotification(title, message);

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}
