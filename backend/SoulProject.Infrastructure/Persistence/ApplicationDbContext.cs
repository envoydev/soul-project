using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace SoulProject.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext, IApplicationDbContext
{
    private readonly ISessionService? _sessionService;
    
    private IDbContextTransaction? _currentTransaction;
    
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, 
        ISessionService? sessionService
        ) 
        : base(options)
    {
        _sessionService = sessionService;
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Soul> Souls => Set<Soul>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<Trait> Traits => Set<Trait>();
    public DbSet<TrustCircle> TrustCircles => Set<TrustCircle>();
    public DbSet<SoulSkill> SoulSkills => Set<SoulSkill>();
    public DbSet<SoulTrait> SoulTraits => Set<SoulTrait>();
    

    public async Task StartTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.DisposeAsync();
        }
        
        _currentTransaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            return;
        }
        
        await _currentTransaction.CommitAsync(cancellationToken);
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction == null)
        {
            return;
        }
        
        await _currentTransaction.RollbackAsync(cancellationToken);
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    #region Overrided methods

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        modelBuilder.Entity<Soul>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.UserId == _sessionService.UserId);
        modelBuilder.Entity<Skill>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.UserId == _sessionService.UserId);
        modelBuilder.Entity<Trait>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.UserId == _sessionService.UserId);
        modelBuilder.Entity<TrustCircle>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.UserId == _sessionService.UserId);
        modelBuilder.Entity<SoulSkill>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.Soul.UserId == _sessionService.UserId);
        modelBuilder.Entity<SoulSkill>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.Skill.UserId == _sessionService.UserId);
        modelBuilder.Entity<SoulTrait>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.Soul.UserId == _sessionService.UserId);
        modelBuilder.Entity<SoulTrait>().HasQueryFilter(entity => _sessionService != null && _sessionService.UserId.HasValue && entity.Trait.UserId == _sessionService.UserId);
    }

    #endregion
}