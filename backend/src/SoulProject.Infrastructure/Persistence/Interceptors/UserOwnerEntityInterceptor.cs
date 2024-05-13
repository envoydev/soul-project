using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SoulProject.Infrastructure.Persistence.Interceptors;

public class UserOwnerEntityInterceptor : SaveChangesInterceptor
{
    private readonly ISessionService _sessionService;

    public UserOwnerEntityInterceptor(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        UpdateEntities(eventData.Context);

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void UpdateEntities(DbContext? context)
    {
        if (context == null || !_sessionService.UserId.HasValue)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<BaseUserOwnerEntity>())
        {
            if (entry.State is not EntityState.Added)
            {
                continue;
            }
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.UserId = _sessionService.UserId.Value;
            }
        }
    }
}