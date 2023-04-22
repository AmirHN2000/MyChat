using Client.App.Services.Contracts;
using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Implementations;

public class AuthenticationService : IAuthenticationService
{
    private readonly AppAuthenticationStateProvider _authenticationStateProvider;

    public AuthenticationService(AppAuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task SignIn(UserInfoVm userInfo)
    {
        StrongService.SetUserInfo(userInfo);
        Preferences.Set("access_token", userInfo.Token);

        await _authenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }

    public async Task SignOut()
    {
        await StrongService.RemoveUserInfo();
        Preferences.Remove("access_token");
        
        await _authenticationStateProvider.RaiseAuthenticationStateHasChanged();
    }

    public async Task<bool> IsUserAuthenticated()
    {
        return await _authenticationStateProvider.IsUserAuthenticatedAsync();
    }
}