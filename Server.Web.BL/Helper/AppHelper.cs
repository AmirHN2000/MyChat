using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Web.BL.Helper;

public static class AppHelper
{
    public static IConfiguration Configuration;

    public static void AddServices(IServiceCollection serviceCollection)
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();

        Configuration = serviceProvider.GetRequiredService<IConfiguration>();
    }
    
    public static string GetFileUrl()
    {
        var url = Configuration["BaseUrl"];
        return url ?? string.Empty;
    }
}