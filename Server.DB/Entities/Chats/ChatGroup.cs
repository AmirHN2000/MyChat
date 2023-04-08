using MyChat.Server.DB.Base;
using MyChat.Server.DB.Entities.Users;

namespace MyChat.Server.DB.Entities.Chats;

public class ChatGroup : BaseEntity
{
    /// <summary>
    /// user create group
    /// </summary>
    public int OwnerId { get; set; }

    /// <summary>
    /// user that receive chat
    /// </summary>
    public int? ReceiverId { get; set; }

    public string GroupName { get; set; }

    public string Image { get; set; }

    public bool IsPrivate { get; set; }

    #region relations

    public User Owner { get; set; }
    
    public User? Receiver { get; set; }

    public List<ChatGroupUser> ChatGroupUsers { get; set; }

    #endregion
}