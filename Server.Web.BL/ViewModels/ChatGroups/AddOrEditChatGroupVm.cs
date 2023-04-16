using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Server.Web.BL.ViewModels.ChatGroups;

public class AddOrEditChatGroupVm
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "نام گروه اجباری است.")]
    public string GroupName { get; set; }

    public bool IsPrivate { get; set; }
    
    public int? ReceiverId { get; set; }
    
    [AllowNull]
    public IFormFile? Image { get; set; }
}