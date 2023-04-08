using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyChat.Server.DB.Entities.Chats.Config;

public class ChatGroupUserConfig : IEntityTypeConfiguration<ChatGroupUser>
{
    public void Configure(EntityTypeBuilder<ChatGroupUser> builder)
    {
        builder.HasOne(x => x.ChatGroup)
            .WithMany(x => x.ChatGroupUsers)
            .HasForeignKey(x => x.ChatGroupId)
            .IsRequired();
        
        builder.HasOne(x => x.User)
            .WithMany(x => x.ChatGroupUsers)
            .HasForeignKey(x => x.UserId)
            .IsRequired();
    }
}