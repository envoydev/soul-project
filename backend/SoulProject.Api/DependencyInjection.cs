using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SoulProject.Api.Extensions;
using SoulProject.Api.Infrastructure.Services;
using SoulProject.Api.Infrastructure.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoulProject.Api;

internal static class DependencyInjection
{
    internal static void AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddApiVersioning(SetApiVersioningOptions)
                .AddApiExplorer(SetApiExplorerOptions);
        services.AddHttpContextAccessor();
        services.AddDataProtection();
        services.AddCors();
        services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
        
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());

        services.AddCarter(configurator: c =>
        {
            c.WithEmptyValidators();
            c.WithEmptyResponseNegotiators();
            c.WithEmptyResponseNegotiators();
        });
        
        services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.SetApplicationJsonSerializeRules();
        });
        
        // Services
        services.AddScoped<ISessionService, SessionService>();
        services.AddSingleton<ISettingsService, SettingsService>();
        services.AddSingleton<IJsonSerializerService, JsonSerializeService>();
    }

    private static void SetApiVersioningOptions(ApiVersioningOptions options)
    {
        options.ReportApiVersions = true;
    }
    
    private static void SetApiExplorerOptions(ApiExplorerOptions options)
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}