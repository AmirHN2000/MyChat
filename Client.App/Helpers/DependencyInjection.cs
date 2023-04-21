using Client.App.Services.Contracts;
using Client.App.Services.Implementations;
using Microsoft.Extensions.Configuration;

namespace Client.App.Helpers;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthTokenProvider, ClientSideAuthTokenProvider>();
        
        services.AddTransient<AppHttpClientHandler>();

        #region services

        services.AddScoped<IUserService, UserService>();

        #endregion
    }
}