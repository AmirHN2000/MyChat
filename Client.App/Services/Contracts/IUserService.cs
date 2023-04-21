using Client.App.Services.Contracts.Base;
using MyChat.Shared.ViewModels.Users;

namespace Client.App.Services.Contracts;

public interface IUserService : IBaseService
{
    Task<string> LogUp(LogUpVm vm);
    Task<UserInfoVm> ConfirmCode(ConfirmCodeAccountVm vm);
}