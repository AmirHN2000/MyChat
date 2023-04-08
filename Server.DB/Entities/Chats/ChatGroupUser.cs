using MyChat.Server.DB.Base;
using MyChat.Server.DB.Entities.Users;

namespace MyChat.Server.DB.Entities.Chats;

public class ChatGroupUser : BaseEntity
{
    public int? ChatGroupId { get; set; }

    public int? UserId { get; set; }

    #region relations

    public ChatGroup ChatGroup { get; set; }

    public User User { get; set; }

    #endregion
}