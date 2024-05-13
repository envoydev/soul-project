using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoulProject.Infrastructure.Persistence.Common;

namespace SoulProject.Infrastructure.Persistence.Configurations;

internal class SoulConfiguration : BaseUserOwnerEntityTypeConfiguration<Soul>
{
    public override void Configure(EntityTypeBuilder<Soul> builder)
    {
        builder.ToTable("Souls");
        
        builder.Property(p => p.TrustCircleId);
        builder.Property(p => p.OccupationId);
        builder.Property(x => x.AvatarPath);
        builder.Property(x => x.FirstName).HasMaxLength(256).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(256);
        builder.Property(x => x.Nickname).HasMaxLength(256);
        builder.Property(x => x.Description);
        builder.Property(x => x.IncomeAmount).HasDefaultValue(0).IsRequired();
        builder.Property(x => x.IncomeCurrency).HasDefaultValue(Currency.Uah).IsRequired();
        builder.Property(x => x.Added).IsRequired();
        builder.Property(x => x.Meet).IsRequired();
        
        builder.HasOne(x => x.TrustCircle)
               .WithMany()
               .HasForeignKey(x => x.TrustCircleId)
               .OnDelete(DeleteBehavior.NoAction);
        
        base.Configure(builder);
    }
}