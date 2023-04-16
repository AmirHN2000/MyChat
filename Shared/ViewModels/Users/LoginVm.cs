using System.ComponentModel.DataAnnotations;

namespace MyChat.Shared.ViewModels.Users;

public class LoginVm
{
    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست.")]
    [Required(ErrorMessage = "وارد کردن ایمیل اجباری است.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "وارد کردن رمز عبور اجباری است.")]
    public string Password { get; set; }
}