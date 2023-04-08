using MyChat.Server.DB.Base;

namespace MyChat.Server.DB.Entities.Chats;

public class ChatFile : BaseEntity
{
    public string Path { get; set; }

    public int ChatId { get; set; }

    #region relations

    public Chat Chat { get; set; }

    #endregion
}