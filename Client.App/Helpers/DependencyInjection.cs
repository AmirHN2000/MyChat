using Client.App.Services.Contracts;
using Client.App.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;

namespace Client.App.Helpers;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAuthTokenProvider, ClientSideAuthTokenProvider>();
        
        services.AddScoped<AuthenticationStateProvider, AppAuthenticationStateProvider>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped(sp => (AppAuthenticationStateProvider)sp.GetRequiredService<AuthenticationStateProvider>());
        
        services.AddTransient<AppHttpClientHandler>();

        #region services

        services.AddScoped<IUserService, UserService>();

        #endregion
    }
}