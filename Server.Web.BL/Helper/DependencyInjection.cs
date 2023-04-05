using Microsoft.Extensions.DependencyInjection;

namespace Server.Web.BL.Helper;

public static class DependencyInjection
{
    public static IServiceCollection AddAllServices(this IServiceCollection serviceCollection)
    {
        // add unit of work

        #region Chat

        // services.AddScoped<IContactMessageRepository, ContactMessageRepository>();
        // services.AddScoped(provider => new Lazy<IContactMessageRepository>(provider.GetService<IContactMessageRepository>));

        #endregion

        return serviceCollection;
    }
}