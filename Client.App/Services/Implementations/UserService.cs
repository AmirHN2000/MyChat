using Client.App.Services.Contracts;
using Client.App.Services.Implementations.Base;
using MudBlazor;

namespace Client.App.Services.Implementations;

public class UserService : BaseService, IUserService
{

    public UserService(HttpClient httpClient, ISnackbar snackbar) : base(httpClient, snackbar)
    {
    }
}