using System.Security.Claims;

namespace MyChat.Server.BL.Helpers;

public static class Extensions
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        var s = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier)
            .Select(x => x.Value).FirstOrDefault();

        int.TryParse(s, out var userId);

        return userId;
    }
    
    public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
    {
        var name = claimsPrincipal.Claims.Where(x => x.Type == ClaimTypes.Name)
            .Select(x => x.Value).FirstOrDefault();

        return name ?? string.Empty;
    }
}