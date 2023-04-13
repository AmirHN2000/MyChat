using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Server.Web.Api.ViewModels.Users;

public class CompleteProfileVm
{
    [Required(ErrorMessage = "وارد کردن نام اجباری است.")]
    [MaxLength(150, ErrorMessage = "طول نام وارد شده زیاد است.")]
    public string Name { get; set; }
    
    [MinLength(3, ErrorMessage = "حداقل طول آیدی 3 کاراکتر است.")]
    [AllowNull]
    public string? UserIdentifire { get; set; }

    [Required(ErrorMessage = "وارد کردن رمز عبور است.")]
    [MinLength(6, ErrorMessage = "حداقل طول رمز عبور 6 کاراکتر است.")]
    public string Password { get; set; }
    
    [Required(ErrorMessage = "وارد کردن تکرار رمز عبور است.")]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
    public string RepeatPassword { get; set; }
    
    [AllowNull]
    public IFormFile? Image { get; set; }
}