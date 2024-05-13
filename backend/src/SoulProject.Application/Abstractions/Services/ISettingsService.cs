namespace SoulProject.Application.Abstractions.Services;

public interface ISettingsService
{
    ApplicationSettings GetSettings();
    EnvironmentRuntime GetCurrentRuntimeEnvironment();
    string GetStaticFilesPath();
}