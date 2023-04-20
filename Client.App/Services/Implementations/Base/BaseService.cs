using System.Net.Http.Json;
using Client.App.Services.Contracts.Base;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using MyChat.Shared.ViewModels;

namespace Client.App.Services.Implementations.Base;

public class BaseService : IBaseService
{
    protected readonly HttpClient HttpClient;
    protected readonly ISnackbar Snackbar;

    public BaseService(HttpClient httpClient, ISnackbar snackbar)
    {
        HttpClient = httpClient;
        Snackbar = snackbar;
    }

    public async Task<T?> Get<T>(string url, bool showMessage = true)
    {
        try
        {
            var apiResult = await HttpClient.GetFromJsonAsync<ResultObject>(url);

            if (apiResult.Success)
            {
                var result = (T) apiResult.Extera;
                if (showMessage)
                    Snackbar.Add(apiResult.Message, Severity.Success);
                
                return result;
            }

            if (showMessage)
                Snackbar.Add(apiResult.Message, Severity.Warning);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            if (showMessage)
                ShowError(e);
        }

        return default;
    }


    private void ShowError(Exception e)
    {
        Snackbar.Add("حطایی رخ داده است : " + e.Message, Severity.Error);
    }
}