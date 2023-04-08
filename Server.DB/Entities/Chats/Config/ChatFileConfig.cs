using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyChat.Server.DB.Entities.Chats.Config;

public class ChatFileConfig : IEntityTypeConfiguration<ChatFile>
{
    public void Configure(EntityTypeBuilder<ChatFile> builder)
    {
        builder.HasOne(x => x.Chat)
            .WithMany(x => x.ChatFiles)
            .HasForeignKey(x => x.ChatId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}