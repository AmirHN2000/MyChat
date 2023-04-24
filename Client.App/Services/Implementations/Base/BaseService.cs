using System.Net.Http.Json;
using Client.App.Services.Contracts.Base;
using MudBlazor;
using MyChat.Shared.ViewModels;
using Newtonsoft.Json;

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

    public async Task<T?> Get<T>(string url, bool showMessage = false)
    {
        try
        {
            var response = await HttpClient.GetFromJsonAsync<ResultObject>(url);

            if (response.Success)
            {
                var result = (T)response.Extera;
                if (showMessage)
                    Snackbar.Add(response.Message, Severity.Success);

                return result;
            }

            Snackbar.Add(response.Message, Severity.Warning);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            ShowError(e);
        }

        return default;
    }

    public async Task<TResponse?> Post<TRequest, TResponse>(string url, TRequest model, bool showMessage = false)
    {
        try
        {
            var response = await HttpClient.PostAsJsonAsync(url, model);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResultObject>();

                if (result.Success)
                {
                    var obj = JsonConvert.DeserializeObject<TResponse>(result.Extera.ToString());

                    if (showMessage)
                        Snackbar.Add(result.Message, Severity.Success);

                    return obj;
                }

                Snackbar.Add(result.Message, Severity.Warning);
            }
            else
            {
                Snackbar.Add("خطا در ارتباط با سرور", Severity.Warning);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);

            ShowError(e);
        }

        return default;
    }

    public async Task<TResponse?> PostFormData<TResponse>(string url, MultipartFormDataContent content,
        bool showMessage = false) where TResponse : class, new()
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Content = content;

        try
        {
            using var response = await HttpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResultObject>();
                if (result.Success)
                {
                    if (result.Extera == null)
                    {
                        return new TResponse();
                    }
                    var obj = JsonConvert.DeserializeObject<TResponse>(result.Extera.ToString());

                    if (showMessage)
                        Snackbar.Add(result.Message, Severity.Success);

                    return obj;
                }

                Snackbar.Add(result.Message, Severity.Warning);
            }
            else
            {
                Snackbar.Add("خطا در ارتباط با سرور", Severity.Warning);
            }
        }
        catch (Exception exp)
        {
            ShowError(exp);
        }

        return default;
    }


    private void ShowError(Exception e)
    {
        Snackbar.Add("حطایی رخ داده است : " + e.Message, Severity.Error);
    }
}