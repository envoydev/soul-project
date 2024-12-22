using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace SoulProject.Infrastructure.Persistence.Interceptors;

internal class AuditableEntityInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeService _dateTimeService;

    public AuditableEntityInterceptor(IDateTimeService dateTimeService)
    {
        _dateTimeService = dateTimeService;
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
        if (context == null)
        {
            return;
        }

        foreach (var entry in context.ChangeTracker.Entries<BaseAuditableEntity>())
        {
            if (entry.State is not EntityState.Added)
            {
                continue;
            }
            
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = _dateTimeService.GetUtc();
            }
        }
    }
}