using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoulProject.Infrastructure.Persistence.Configurations;

public class SoulSkillConfiguration : IEntityTypeConfiguration<SoulSkill>
{
    public void Configure(EntityTypeBuilder<SoulSkill> builder)
    {
        builder.ToTable("SoulSkills");
        
        builder.HasKey(x => new { x.SoulId, x.SkillId });
        
        builder.Property(x => x.Level).IsRequired().HasDefaultValue(SkillLevel.None);

        builder.HasOne(x => x.Soul)
               .WithMany(x => x.SoulSkills)
               .HasForeignKey(x => x.SoulId)
               .OnDelete(DeleteBehavior.ClientCascade);
        
        builder.HasOne(x => x.Skill)
               .WithMany()
               .HasForeignKey(x => x.SkillId)
               .OnDelete(DeleteBehavior.ClientCascade);
    }
}