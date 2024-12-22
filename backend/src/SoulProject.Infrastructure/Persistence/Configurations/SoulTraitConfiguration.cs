using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoulProject.Infrastructure.Persistence.Configurations;

public class SoulTraitConfiguration : IEntityTypeConfiguration<SoulTrait>
{
    public void Configure(EntityTypeBuilder<SoulTrait> builder)
    {
        builder.ToTable("SoulTraits");
        
        builder.HasKey(x => new { x.SoulId, x.TraitId });

        builder.HasOne(x => x.Soul)
               .WithMany(x => x.SoulTraits)
               .HasForeignKey(x => x.SoulId)
               .OnDelete(DeleteBehavior.ClientCascade);
        
        builder.HasOne(x => x.Trait)
               .WithMany()
               .HasForeignKey(x => x.TraitId)
               .OnDelete(DeleteBehavior.ClientCascade);
    }
}