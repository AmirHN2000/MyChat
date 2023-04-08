using MyChat.Server.DB.Base;
using MyChat.Server.DB.Entities.Users;
using MyChat.Shared.Enums.Chats;

namespace MyChat.Server.DB.Entities.Chats;

public class Chat : BaseEntity
{
    public int UserId { get; set; }

    public string Body { get; set; }

    public ChatStatus ChatStatus { get; set; }

    #region relations

    public User User { get; set; }

    public List<ChatFile> ChatFiles { get; set; }

    #endregion
}