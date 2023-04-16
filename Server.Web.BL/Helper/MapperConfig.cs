using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using MyChat.Server.DB.Entities.Chats;
using Server.Web.BL.ViewModels.ChatGroups;

namespace Server.Web.BL.Helper;

public static class MapperConfig
{
    public static void AddMapperConfig(this IServiceCollection services)
    {
        var config = new TypeAdapterConfig();


        #region ChatGroup

        // config.NewConfig<AddChatGroupVm, ChatGroup>()
        //     .Map(dest => dest.OwnerId, src => User)

        #endregion

        #region Chat

        

        #endregion
        
        
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }
}