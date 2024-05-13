using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using SoulProject.Api.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SoulProject.Api.Infrastructure.Swagger;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = "Example API",
                Description = "An example API",
                Version = description.ApiVersion.ToString(),
            };
            
            options.SwaggerDoc(description.GroupName, openApiInfo);
            options.AddSecurityDefinition(HttpItemsConstants.AuthenticationScheme, GetOpenApiSecurityScheme());
            options.AddSecurityRequirement(GetOpenApiSecurityRequirement());
        }
    }
    
    private static OpenApiSecurityScheme GetOpenApiSecurityScheme()
    {
        return new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = HttpItemsConstants.AuthenticationScheme,
            Name = HttpItemsConstants.AuthorizationHeaderName,
            BearerFormat = HttpItemsConstants.BearerFormat,
            Description = "Please enter a valid token"
        };
    }
    
    private static OpenApiSecurityRequirement GetOpenApiSecurityRequirement()
    {
        return new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = HttpItemsConstants.AuthenticationScheme
                    }
                },
                Array.Empty<string>()
            }
        };
    }
}