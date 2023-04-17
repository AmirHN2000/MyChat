using System.Reflection;
using Microsoft.Extensions.Logging;
using Client.App.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using MudBlazor.Services;

namespace Client.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        var assembly = typeof(MainLayout).GetTypeInfo().Assembly;

        var settings = "appsettings.json";
#if DEBUG
        settings = "appsettings.Development.json";
#endif
        
        builder
            .UseMauiApp<App>()
            .Configuration.AddJsonFile(new EmbeddedFileProvider(assembly), settings, optional: false, false);

        builder.Services.AddMauiBlazorWebView();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMudServices();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}