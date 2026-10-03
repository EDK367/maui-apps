using Microsoft.Extensions.Logging;
using parcial2Progra.Data;
using parcial2Progra.Interfaces;
using parcial2Progra.Service;
using parcial2Progra.ViewModels;
using parcial2Progra.Views;

namespace parcial2Progra;

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
        builder.Services.AddSingleton<INotificationManagerService, parcial2Progra.Platforms.Android.NotificationManagerService>();
#endif

        builder.Services.AddSingleton<DatabaseService>();

        builder.Services.AddSingleton<AlumnoService>();
        builder.Services.AddSingleton<CursoService>();
        builder.Services.AddSingleton<AsignacionService>();

        builder.Services.AddTransient<HomeViewModel>();
        builder.Services.AddTransient<AlumnosViewModel>();
        builder.Services.AddTransient<CursosViewModel>();
        builder.Services.AddTransient<AsignacionesViewModel>();

        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<AlumnosPage>();
        builder.Services.AddTransient<CursosPage>();
        builder.Services.AddTransient<AsignacionesPage>();
        builder.Services.AddTransient<UsuariosPage>();
        builder.Services.AddTransient<AcercaPage>();

        return builder.Build();
    }
}
