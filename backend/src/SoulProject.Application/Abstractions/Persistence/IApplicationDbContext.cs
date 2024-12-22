using Microsoft.EntityFrameworkCore;

namespace SoulProject.Application.Abstractions.Persistence;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Soul> Souls { get; }
    DbSet<Skill> Skills { get; }
    DbSet<Trait> Traits { get; }
    DbSet<TrustCircle> TrustCircles { get; }
    DbSet<SoulSkill> SoulSkills { get; }
    DbSet<SoulTrait> SoulTraits { get; }

    Task<int> SaveChangesAsync();
    
    Task StartTransactionAsync();
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    
    
}