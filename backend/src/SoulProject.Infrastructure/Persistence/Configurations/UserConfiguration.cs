using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoulProject.Infrastructure.Persistence.Common;

namespace SoulProject.Infrastructure.Persistence.Configurations;

internal class UserConfiguration : BaseAuditableEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.Property(x => x.FirstName).HasMaxLength(256);
        builder.Property(x => x.LastName).HasMaxLength(256);
        builder.Property(x => x.UserName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Role).IsRequired().HasDefaultValue(Role.User);
        builder.Property(x => x.PasswordHash).HasMaxLength(256).IsRequired();
        builder.Property(x => x.RefreshToken).HasMaxLength(1024);
        builder.Property(x => x.RefreshTokenExpiration);

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.UserName).IsUnique();
        
        base.Configure(builder);
    }
}