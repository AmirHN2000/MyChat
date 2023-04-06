using Microsoft.Extensions.DependencyInjection;
using MyChat.Server.DB;
using MyChat.Shared.Interface;

namespace Server.Web.BL.Helper;

public static class DependencyInjection
{
    public static IServiceCollection AddAllServices(this IServiceCollection services)
    {
        #region Public

        services.AddScoped<IDbInitializerService, DbInitializerService>();
        services.AddScoped(provider => new Lazy<IDbInitializerService>(provider.GetService<IDbInitializerService>));

        // add unit of work
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(provider => new Lazy<IUnitOfWork>(provider.GetService<IUnitOfWork>));
        
        #endregion
        
        #region Chat

        // services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
        // services.AddScoped(provider => new Lazy<IContactMessageRepository>(provider.GetService<IContactMessageRepository>));

        #endregion

        return services;
    }
}