using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyChat.Server.BL.Helpers;
using MyChat.Server.BL.Helpers.FileManager;
using MyChat.Server.DB.Entities.Users;
using MyChat.Shared.ViewModels.Users;
using Server.Web.Api.Helper;
using Server.Web.BL.Helper;
using Server.Web.BL.ViewModels.Users;

namespace Server.Web.Api.Controllers;

public class UserController : BaseApiController
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public UserController(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody]LoginVm vm)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = ModelState.GetErrors()
            });
        }

        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = "کاربر یافت نشد."
            });
        }

        var result = await _userManager.CheckPasswordAsync(user, vm.Password);
        if (result)
        {
            
            var userInfo = await _userManager.Users.Where(x => x.Email == vm.Email)
                .Select(x => new UserInfoVm
                {
                    UserId = x.Id,
                    UserIdentifire = x.UserIdentifire,
                    Name = x.Name,
                    Image = x.Image,
                }).FirstOrDefaultAsync();
            
            if (!string.IsNullOrEmpty(userInfo!.Image))
            {
                userInfo.Image = AppHelper.GetFileUrl() + userInfo.Image;
            }
            
            var claims = await GetClaims(user);
            var token = GetToken(claims);
            userInfo.Token = token;
            
            return Json(new ResultObject()
            {
                Success = true,
                Extera = userInfo
            });
        }
        
        return Json(new ResultObject()
        {
            Success = false,
            Extera = "نام کاربری یا رمز عبور صحیح نمی باشد."
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> LogUp([FromBody]LogUpVm vm)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = ModelState.GetErrors()
            });
        }

        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user != null)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = "اکانت با این ایمیل وجود دارد."
            });
        }
        
        user = new User()
        {
            Email = vm.Email,
            UserName = vm.Email,
            CreationDate = DateTime.Now
        };
        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = "خطا در ارسال کد تایید"
            });
        }
            
        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        
        return Json(new ResultObject()
        {
            Success = true,
            Extera = code
        });
    }
    
    [HttpPost]
    public async Task<IActionResult> ConfirmCode([FromBody]ConfirmCodeAccountVm vm)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = ModelState.GetErrors()
            });
        }

        var user = await _userManager.FindByEmailAsync(vm.Email);
        if (user == null)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = "اکانت یافت نشد."
            });
        }

        var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, vm.Code);
        if (!resultConfirmEmail.Succeeded)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = resultConfirmEmail.GetErrors()
            });
        }
        
        var claims = await GetClaims(user);
        var token = GetToken(claims);

        var result = new UserInfoVm()
        {
            UserId = user.Id,
            Token = token
        };
        
        return Json(new ResultObject()
        {
            Success = true,
            Message = "ورود موفقیت آمیز است.",
            Extera = result
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CompleteProfile([FromForm]CompleteProfileVm vm)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = ModelState.GetErrors()
            });
        }

        var user = await _userManager.FindByIdAsync(User.GetUserId().ToString());
        if (user == null)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = "کاربر یافت نشد."
            });
        }

        if (!string.IsNullOrEmpty(vm.UserIdentifire) && !string.IsNullOrWhiteSpace(vm.UserIdentifire))
        {
            var existUserName = await _userManager.Users.AnyAsync(x => x.UserIdentifire.Equals(vm.UserIdentifire));
            if (existUserName)
            {
                return Json(new ResultObject()
                {
                    Success = false,
                    Message = "این نام کاربری قبلا انتخاب شده است."
                });
            }
        }
        
        var resultSetPassword = await _userManager.AddPasswordAsync(user, vm.Password);
        if (!resultSetPassword.Succeeded)
        {
            return Json(new ResultObject()
            {
                Success = false,
                Message = resultSetPassword.GetErrors()
            });
        }

        if (vm.Image != null)
        {
            if (vm.Image.Length > 2*1024*1024)
            {
                return Json(new ResultObject()
                {
                    Success = false,
                    Message = "عکس پروفایل نباید بیشتر از 2 مگابایت باشد."
                });
            }

            var path = FileUploadPath.ProfileImage + User.GetUserId() + "/";
            user.Image = await vm.Image.SaveImage(path);
        }
        
        user.UserIdentifire = vm.UserIdentifire;
        user.Name = vm.Name;

        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            await _userManager.RemovePasswordAsync(user);
            return Json(new ResultObject()
            {
                Success = false,
                Message = result.GetErrors()
            });
        }

        return Json(new ResultObject
        {
            Success = true,
            Message = "اکانت با موفقیت تکمیل شد."
        });
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetUserInfo()
    {
        var userInfo = await _userManager.Users.Where(x => x.Id == User.GetUserId())
            .Select(x => new UserInfoVm
            {
                UserId = x.Id,
                UserIdentifire = x.UserIdentifire,
                Name = x.Name,
                Image = x.Image,
            }).FirstOrDefaultAsync();

        if (!string.IsNullOrEmpty(userInfo!.Image))
        {
            userInfo.Image = AppHelper.GetFileUrl() + userInfo.Image;
        }
        
        if (userInfo != null)
        {
            return Json(new ResultObject
            {
                Success = true,
                Extera = userInfo
            });
        }
        return Json(new ResultObject
        {
            Success = false,
            Message = "کاربر یافت نشد."
        });
    }
    
    [NonAction]
    private async Task<List<Claim>> GetClaims(User user)
    {
        var list = new List<Claim>();

        list.AddRange(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName)
        });
        var roles = await _userManager.GetRolesAsync(user);
        list.AddRange(roles.Select(x => new Claim("role", x)));

        var claims = await _userManager.GetClaimsAsync(user);
        list.AddRange(claims);


        return list;
    }

    [NonAction]
    private string GetToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:Expire"])), signingCredentials: credentials);
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return accessToken;
    }
}