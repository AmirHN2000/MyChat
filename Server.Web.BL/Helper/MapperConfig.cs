using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Server.Web.BL.Helper;

public static class MapperConfig
{
    public static void AddMapperConfig(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();



        #region Chat

        

        #endregion
        
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}