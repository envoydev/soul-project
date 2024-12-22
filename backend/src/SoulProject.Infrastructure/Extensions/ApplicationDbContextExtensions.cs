using Microsoft.EntityFrameworkCore;
using SoulProject.Infrastructure.Persistence;

namespace SoulProject.Infrastructure.Extensions;

internal static class ApplicationDbContextExtensions
{
    internal static void SetDatabaseType(this DbContextOptionsBuilder<ApplicationDbContext> builder, string connectionString)
    {
        builder.UseSqlite(connectionString);
    }
    
    internal static void SetDatabaseType(this DbContextOptionsBuilder builder, string connectionString)
    {
        builder.UseSqlite(connectionString);
    }
}