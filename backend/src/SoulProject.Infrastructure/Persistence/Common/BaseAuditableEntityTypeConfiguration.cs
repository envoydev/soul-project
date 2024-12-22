using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SoulProject.Infrastructure.Persistence.Common;

internal class BaseAuditableEntityTypeConfiguration<TEntity> : BaseEntityTypeConfiguration<TEntity> where TEntity : BaseAuditableEntity
{
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(x => x.CreatedDate).IsRequired().HasColumnOrder(2);
    }
}