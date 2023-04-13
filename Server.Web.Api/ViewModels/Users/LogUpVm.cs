using System.ComponentModel.DataAnnotations;

namespace Server.Web.Api.ViewModels.Users;

public class LogUpVm
{
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست.")]
    [Required(ErrorMessage = "وارد کردن ایمیل اجباری است.")]
    public string Email { get; set; }
}