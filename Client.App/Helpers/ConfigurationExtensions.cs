using Microsoft.Extensions.Configuration;

namespace Client.App.Helpers;

public static class ConfigurationExtensions
{
    public static string GetApiServerAddress(this IConfiguration configuration)
    {
        return configuration.GetValue<string?>("ApiServerAddress") ?? string.Empty;
    }
}