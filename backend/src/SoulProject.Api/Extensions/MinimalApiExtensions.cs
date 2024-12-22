using SoulProject.Application.Security;

namespace SoulProject.Api.Extensions;

public static class MinimalApiExtensions
{
    public static TBuilder AuthorizationRequired<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Add(convention => convention.Metadata.Add(new AuthorizeAttribute()));

        return builder;
    }
}