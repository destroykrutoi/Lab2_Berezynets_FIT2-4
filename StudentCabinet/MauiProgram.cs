using Microsoft.Extensions.Logging;
using StudentCabinet.ViewModels;
using StudentCabinet.Views;

namespace StudentCabinet;

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

        // сторінка деталей та її ViewModel створюються через DI
        builder.Services.AddTransient<StudentDetailViewModel>();
        builder.Services.AddTransient<StudentDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
