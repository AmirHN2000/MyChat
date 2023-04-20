using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyChat.Server.BL.Helpers;
using MyChat.Server.BL.Helpers.FileManager;
using MyChat.Server.DB.Entities.Chats;
using MyChat.Shared.ViewModels;
using Server.Web.Api.Helper;
using Server.Web.BL;
using Server.Web.BL.ViewModels.ChatGroups;

namespace Server.Web.Api.Controllers;

[Authorize]
public class ChatGroupController : BaseApiController
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ChatGroupController(UnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> AddOrEditChatGroup([FromForm] AddOrEditChatGroupVm vm)
    {
        if (!ModelState.IsValid)
        {
            return Json(new ResultObject
            {
                Success = false,
                Message = ModelState.GetErrors()
            });
        }

        var model = _mapper.Map<ChatGroup>(vm);
        model.OwnerId = User.GetUserId();

        if (vm.Image != null)
        {
            if (vm.Image.Length > 2*1024*1024)
            {
                return Json(new ResultObject()
                {
                    Success = false,
                    Message = "عکس گروه نباید بیشتر از 2 مگابایت باشد."
                });
            }

            var path = FileUploadPath.GroupImage + "/";
            model.Image = await vm.Image.SaveImage(path);
        }

        await _unitOfWork.ChatGroupRepository.InsertAsync(model);

        var result = await _unitOfWork.SaveAsync();

        if (result > 0)
        {
            return Json(new ResultObject
            {
                Success = true,
                Message = "گروه با موفقیت ایجاد شد.",
                Extera = model.Id
            });
        }
        
        return Json(new ResultObject
        {
            Success = false,
            Message = "گروه ایجاد نشد."
        });
    }
}