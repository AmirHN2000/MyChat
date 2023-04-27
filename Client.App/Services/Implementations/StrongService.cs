using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Implementations;

public class StrongService
{
    public static void SetUserInfo(UserInfoVm userInfo)
    {
        Preferences.Set("UserId", userInfo.UserId);
        Preferences.Set("Name", userInfo.Name);
        Preferences.Set("UserIdentifire", userInfo.UserIdentifire);
        Preferences.Set("Image", userInfo.Image);
    }

    public static async Task<UserInfoVm> GetUserInfo()
    {
        var result = new UserInfoVm();

        result.UserId = await Task.FromResult(Preferences.Get("UserId", 0));
        result.Name = await Task.FromResult(Preferences.Get("Name", string.Empty));
        result.UserIdentifire = await Task.FromResult(Preferences.Get("UserIdentifire", string.Empty));
        result.Image = await Task.FromResult(Preferences.Get("Image", string.Empty));

        return result;
    }

    public static async Task RemoveUserInfo()
    {
        await Task.FromResult(() =>
        {
            Preferences.Remove("UserId");
            Preferences.Remove("Name");
            Preferences.Remove("UserIdentifire");
            Preferences.Remove("Image");
        });
    }
}