using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Server.Web.Api.Helper;

public static class Extensions
{
    public static string GetErrors(this ModelStateDictionary modelState)
    {
        string result = "";
        var errors = modelState.SelectMany(s => s.Value.Errors).ToArray();
        for (int i = 0; i < errors.Count(); i++)
        {
            result += errors[i].ErrorMessage + Environment.NewLine;
        }
        return result;
    }
    
    public static string GetErrors(this IdentityResult identityResult)
    {
        string result = "";
        var errors = identityResult.Errors.Select(x => x.Description).ToArray();
        for (int i = 0; i < errors.Count(); i++)
        {
            result += errors[i] + Environment.NewLine;
        }
        return result;
    }
}