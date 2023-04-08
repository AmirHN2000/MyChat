using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyChat.Server.DB.Entities.Chats.Config;

public class ChatGroupConfig : IEntityTypeConfiguration<ChatGroup>
{
    public void Configure(EntityTypeBuilder<ChatGroup> builder)
    {
        builder.Property(x => x.Image).IsRequired(false);

        builder.Property(x => x.GroupName).IsRequired()
            .HasMaxLength(150);

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.ChatGroupOwners)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
        
        builder.HasOne(x => x.Receiver)
            .WithMany(x => x.ChatGroupReceivers)
            .HasForeignKey(x => x.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);
    }
}