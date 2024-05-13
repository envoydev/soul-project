using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoulProject.Infrastructure.Persistence.Common;

namespace SoulProject.Infrastructure.Persistence.Configurations;

internal class TraitConfiguration : BaseUserOwnerEntityTypeConfiguration<Trait>
{
    public override void Configure(EntityTypeBuilder<Trait> builder)
    {
        builder.ToTable("Traits");
        
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Side).IsRequired();
        
        builder.HasIndex(x => new { x.Name, x.Side }).IsUnique();
        
        base.Configure(builder);
    }
}