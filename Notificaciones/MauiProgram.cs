using Microsoft.Extensions.Logging;
using Notificaciones.Interfaces;

namespace Notificaciones
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

#if ANDROID
    builder.Services.AddSingleton<INotificationManagerService, Notificaciones.Platforms.Android.NotificationManagerService>();
#endif

            return builder.Build();
        }
    }
}
