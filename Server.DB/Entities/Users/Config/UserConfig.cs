using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyChat.Server.DB.Entities.Users.Config;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.UserName).IsRequired(false);
        builder.Ignore(x => x.PhoneNumber);
        builder.Ignore(x => x.PhoneNumberConfirmed);
        
        builder.Property(x => x.Name).IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Image).IsRequired(false);

        builder.Property(x => x.Bio).IsRequired(false)
            .HasMaxLength(500);
    }
}