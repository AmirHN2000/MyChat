using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyChat.Server.DB;
using MyChat.Server.DB.Entities.Role;
using MyChat.Server.DB.Entities.User;

// namespace Server.Api.Helper;
//
// public static class DataBaseConfig
// {
//     public static WebApplicationBuilder AddDbConfig(this WebApplicationBuilder webApplicationBuilder)
//     {
//         webApplicationBuilder.Services.AddDbContext<AppDbContext>(options =>
//         {
//             options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("Default"));
//         });
//
//         webApplicationBuilder.Services.AddIdentity<User, Role>(options =>
//             {
//                 options.Lockout.AllowedForNewUsers = false;
//                 options.Password.RequireDigit = false;
//                 options.Password.RequiredLength = 6;
//                 options.Password.RequireLowercase = false;
//                 options.Password.RequireUppercase = false;
//                 options.Password.RequiredUniqueChars = 0;
//                 options.Password.RequireNonAlphanumeric = false;
//                 options.User.RequireUniqueEmail = true;
//                 options.SignIn.RequireConfirmedAccount = false;
//                 options.SignIn.RequireConfirmedEmail = false;
//                 options.SignIn.RequireConfirmedPhoneNumber = false;
//             })
//             .AddRoles<Role>()
//             .AddEntityFrameworkStores<AppDbContext>()
//             .AddDefaultTokenProviders();
//         
//             // add persian error identity
//             //.AddErrorDescriber<CustomIdentityError>();
//
//         return webApplicationBuilder;
//     }
// }