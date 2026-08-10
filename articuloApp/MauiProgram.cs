using articuloApp.data;
using articuloApp.view;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace articuloApp;

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

        builder.Services.AddDbContext<AppDbContext>();
        builder.Services.AddTransient<FabricanteView>();
        builder.Services.AddTransient<ArticuloView>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }

        return app;
    }
}

