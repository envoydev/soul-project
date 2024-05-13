using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoulProject.Infrastructure.Persistence.Common;

namespace SoulProject.Infrastructure.Persistence.Configurations;

internal class TrustCircleConfiguration : BaseUserOwnerEntityTypeConfiguration<TrustCircle>
{
    public override void Configure(EntityTypeBuilder<TrustCircle> builder)
    {
        builder.ToTable("TrustCircles");
        
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Description);
        builder.Property(x => x.PositionIndex).IsRequired();
        builder.Property(x => x.Color).HasMaxLength(20).IsRequired();
        
        builder.HasIndex(x => x.Name).IsUnique();
        
        base.Configure(builder);
    }
}