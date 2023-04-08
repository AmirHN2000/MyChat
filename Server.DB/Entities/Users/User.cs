using Microsoft.AspNetCore.Identity;
using MyChat.Server.DB.Entities.Chats;

namespace MyChat.Server.DB.Entities.Users;

public class User : IdentityUser<int>
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string Bio { get; set; }

    public DateTime CreationDate { get; set; }

    #region relations

    public List<ChatGroup> ChatGroupOwners { get; set; }
    public List<ChatGroup> ChatGroupReceivers { get; set; }
    public List<Chat> Chats { get; set; }
    public List<ChatGroupUser> ChatGroupUsers { get; set; }

    #endregion
}