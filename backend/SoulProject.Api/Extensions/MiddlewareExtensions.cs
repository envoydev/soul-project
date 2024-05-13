using SoulProject.Api.Infrastructure.Middlewares;

namespace SoulProject.Api.Extensions;

public static class MiddlewareExtensions
{
    public static void UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<GlobalExceptionMiddleware>();
    }
    
    public static void UseJwtBearerAuthorization(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<JwtBearerMiddleware>();
    }
}