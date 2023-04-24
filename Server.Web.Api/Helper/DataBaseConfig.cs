using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChat.Server.BL.Helpers.IdentityErrorDescriber;
using MyChat.Server.DB;
using MyChat.Server.DB.Entities.Role;
using MyChat.Server.DB.Entities.Users;

namespace Server.Web.Api.Helper;

public static class DataBaseConfig
{
    public static void AddDbConfig(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        // serviceCollection.AddDbContext<AppDbContext>(options =>
        // {
        //     options.UseSqlServer(configuration.GetConnectionString("DefaultMSSql"));
        // });

#if DEBUG
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultPostgreSql"));
        });  
         
        #else
        
        serviceCollection.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(Environment.GetEnvironmentVariable("db"));
        }); 
        
#endif

        serviceCollection.AddIdentity<User, Role>(options =>
            {
                options.Lockout.AllowedForNewUsers = false;
                
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                
                options.User.RequireUniqueEmail = true;
                
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultPhoneProvider;
                
                options.SignIn.RequireConfirmedAccount = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            })
            .AddRoles<Role>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<CustomIdentityError>();
    }
}