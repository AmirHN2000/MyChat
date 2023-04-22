using Microsoft.Extensions.Configuration;

namespace Client.App.Helpers;

public static class ConfigurationExtensions
{
    public static string GetApiServerAddress(this IConfiguration configuration)
    {
        var key = "ApiServerAddress";
#if ANDROID
        key = "ApiServerAddressMobile";
#endif
        return configuration.GetValue<string?>(key) ?? string.Empty;
    }
}