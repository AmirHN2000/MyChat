using System.ComponentModel.DataAnnotations;

namespace MyChat.Shared.ViewModels.Users;

public class ConfirmCodeAccountVm
{
    [Required(ErrorMessage = "کد تایید را وارد کنید.")]
    [MinLength(6, ErrorMessage = "طول کد تایید 6 کاراکتر می باشد.")]
    [MaxLength(6, ErrorMessage = "طول کد تایید 6 کاراکتر می باشد.")]
    public string Code { get; set; }

    [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نیست.")]
    [Required(ErrorMessage = "وارد کردن ایمیل اجباری است.")]
    public string Email { get; set; }
}