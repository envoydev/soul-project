using Asp.Versioning;
using Asp.Versioning.Builder;

namespace SoulProject.Api.Extensions;

public static class EndpointRouteBuilderExtensions
{
    public static ApiVersionSet GetApplicationApiVersionSet(this IEndpointRouteBuilder app)
    {
        return app.NewApiVersionSet()
                  .HasApiVersion(new ApiVersion(1))
                  .ReportApiVersions()
                  .Build();
    }
}