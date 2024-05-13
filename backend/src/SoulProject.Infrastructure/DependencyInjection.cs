using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Core;
using Serilog.Formatting;
using SoulProject.Infrastructure.Extensions;
using SoulProject.Infrastructure.Logger;
using SoulProject.Infrastructure.Logger.Enrichers;
using SoulProject.Infrastructure.Logger.Formatters;
using SoulProject.Infrastructure.Persistence;
using SoulProject.Infrastructure.Persistence.Interceptors;
using SoulProject.Infrastructure.Services;

namespace SoulProject.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Database
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, UserOwnerEntityInterceptor>();
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            options.AddInterceptors(serviceProvider.GetServices<ISaveChangesInterceptor>());
            options.SetDatabaseType(configuration.GetDatabaseConnectionString());
        });
        services.AddScoped<IApplicationDbContext>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddTransient<IApplicationDbContextInitializer, ApplicationDbContextInitializer>();
        
        // Logger
        services.AddSingleton<ITextFormatter, SerilogJsonFormatter>();
        services.AddSingleton<ILogEventEnricher, TaskIdEnricher>();
        services.AddSingleton<ILogEventEnricher, HttpContextEnricher>();
        services.AddSingleton<SerilogLoggerConfiguration>();
        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilogLogging());
        services.AddScoped(typeof(IApplicationLogger<>), typeof(ApplicationLogger<>));

        // Services
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddSingleton<IDateTimeService, DateTimeService>();
    }
}