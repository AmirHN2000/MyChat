using Client.App.Services.Contracts;
using Client.App.Services.Implementations.Base;
using MudBlazor;
using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Implementations;

public class UserService : BaseService, IUserService
{

    public UserService(HttpClient httpClient, ISnackbar snackbar) : base(httpClient, snackbar)
    {
    }

    public async Task<string> LogUp(LogUpVm vm)
    {
        var result = await Post<LogUpVm, string>("User/LogUp", vm, true);

        return result;
    }

    public async Task<UserInfoVm> ConfirmCode(ConfirmCodeAccountVm vm)
    {
        var result = await Post<ConfirmCodeAccountVm, UserInfoVm>("User/ConfirmCode", vm);

        return result;
    }

    public async Task<UserInfoVm> LogIn(LoginVm vm)
    {
        var result = await Post<LoginVm, UserInfoVm>("User/LogIn", vm);

        return result;
    }
}