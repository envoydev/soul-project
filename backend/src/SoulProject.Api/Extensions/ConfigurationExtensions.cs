using Ardalis.GuardClauses;

namespace SoulProject.Api.Extensions;

public static class ConfigurationExtensions
{
    public static ApplicationSettings GetApplicationSettings(this IConfiguration configuration)
    {
        var applicationSettings = configuration.Get<ApplicationSettings>();

        Guard.Against.Null(applicationSettings, message: $"{nameof(ApplicationSettings)} cannot be null.");

        return applicationSettings;
    }
}