namespace SoulProject.Application.Abstractions.Persistence;

public interface IApplicationDbContextInitializer
{
    Task InitialiseAsync();
    Task SeedAsync();
}