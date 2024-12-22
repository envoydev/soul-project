using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SoulProject.Application.Behaviors;

namespace SoulProject.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        services.AddValidatorsFromAssembly(assembly);
        
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(assembly);
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));
        });

        TypeAdapterConfig.GlobalSettings.Scan(assembly);
        services.AddMapster();
    }
}