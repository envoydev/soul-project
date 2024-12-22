using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoulProject.Infrastructure.Persistence.Common;

internal class BaseUserOwnerEntityTypeConfiguration<TEntity> : BaseAuditableEntityTypeConfiguration<TEntity> where TEntity : BaseUserOwnerEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.UserId).IsRequired().HasColumnOrder(1);

        builder.HasOne<User>()
               .WithMany()
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.ClientCascade);
    }
}