using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Contracts;

public interface IAuthenticationService
{
    Task SignIn(UserInfoVm userInfo);

    Task SignOut();

    Task<bool> IsUserAuthenticated();
}