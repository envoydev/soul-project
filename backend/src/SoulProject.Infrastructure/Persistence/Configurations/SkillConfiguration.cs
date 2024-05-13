using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SoulProject.Infrastructure.Persistence.Common;

namespace SoulProject.Infrastructure.Persistence.Configurations;

internal class SkillConfiguration : BaseUserOwnerEntityTypeConfiguration<Skill>
{
    public override void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills");
        
        builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
        builder.Property(x => x.Description);
        
        base.Configure(builder);
    }
}