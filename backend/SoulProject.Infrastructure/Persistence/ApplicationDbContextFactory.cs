using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SoulProject.Infrastructure.Extensions;

namespace SoulProject.Infrastructure.Persistence;

// ReSharper disable once UnusedType.Global
internal class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var environment = GetEnvironment(args);
        
        Console.WriteLine($"Environment: {environment}");
        
        var parseResult = Enum.TryParse(environment.AsSpan(), out EnvironmentRuntime environmentRuntime);
        
        Guard.Against.Expression(result => result == false, parseResult, message: $"Environment argument is wrong. Argument: {environment}");
        
        var solutionFolder = Directory.GetParent(Directory.GetCurrentDirectory())?.FullName;
        
        Guard.Against.NullOrWhiteSpace(solutionFolder, message: "Root project folder does not exist.");

        var appSettingsFolder = Path.Combine(solutionFolder, ApplicationConstants.AppSettingsProjectName);
        
        Console.WriteLine($"App-Settings path: {appSettingsFolder}");

        var jsonFileName = $"appsettings.{environmentRuntime}.json";

        Console.WriteLine($"App-Settings file name: {jsonFileName}");
        
        var isJsonFileExist = File.Exists(Path.Combine(appSettingsFolder, jsonFileName));
        
        Guard.Against.Expression(result => result == false, isJsonFileExist,  message: "Path to app-settings file does not exist.");
        
        var configurationBuilder = new ConfigurationBuilder();
        var configuration = configurationBuilder.SetBasePath(appSettingsFolder)
                                                .AddJsonFile(jsonFileName, optional: true)
                                                .Build();
        
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();

        builder.SetDatabaseType(configuration.GetDatabaseConnectionString());
        
        return new ApplicationDbContext(builder.Options, null);
    }
    
    private static string GetEnvironment(IEnumerable<string> args)
    {
        var environmentArgs = args.FirstOrDefault(x => x.Contains(ApplicationConstants.EnvironmentArgs));
        if (environmentArgs == null)
        {
            return string.Empty;
        }

        var split = environmentArgs.Split('=');
        return split.Length <= 1 ? string.Empty : split[1];
    }
}