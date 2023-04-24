using System.Net.Http.Headers;
using System.Reflection;
using Client.App.Services.Contracts;
using Client.App.Services.Implementations.Base;
using Client.App.ViewModels;
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

    public async Task<object> CompleteProfile(CompleteProfileClientVm vm)
    {
        try
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            if (vm.PicFile != null)
            {
                content.Add(new StreamContent(new System.IO.MemoryStream(vm.PicFile.Buffers), vm.PicFile.Buffers.Length),
                    $"PicFile", vm.PicFile.File.Name);
            }

            PropertyInfo[] propertyInfos = vm.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                var data = typeof(CompleteProfileClientVm).GetProperty(propertyInfo.Name).GetValue(vm);
                if (data != null)
                    content.Add(new StringContent(data.ToString()), propertyInfo.Name);

            }

            var result = await PostFormData<object?>("User/CompleteProfile", content);
            
            return result;
        }
        catch (Exception ex)
        {
            Snackbar.Add("خطا در انجام عملیات", Severity.Error);
        }

        return default;
    }
}