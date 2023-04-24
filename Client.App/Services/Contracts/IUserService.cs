using Client.App.Services.Contracts.Base;
using Client.App.ViewModels;
using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Contracts;

public interface IUserService : IBaseService
{
    Task<string> LogUp(LogUpVm vm);
    Task<UserInfoVm> ConfirmCode(ConfirmCodeAccountVm vm);
    Task<UserInfoVm> LogIn(LoginVm vm);
    Task<object> CompleteProfile(CompleteProfileClientVm vm);
}