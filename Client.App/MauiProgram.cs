using System.Reflection;
using Client.App.Helpers;
using Client.App.Services;
using Client.App.Services.Implementations;
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
            .ConfigureFonts(fonts => { fonts.AddFont("IRANSansDN.ttf", "IRANSansDN"); });
        
        builder.Services.AddScoped(sp =>
        {
            HttpClient httpClient = new(sp.GetRequiredService<AppHttpClientHandler>())
            {
                BaseAddress = new Uri($"{sp.GetRequiredService<IConfiguration>().GetApiServerAddress()}")
            };

            return httpClient;
        });

        builder.Services.AddMudServices();

        builder.Services.AddServices(builder.Configuration);

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}