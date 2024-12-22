namespace SoulProject.Application.Models.Settings;

public class ApplicationSettings
{
    public ConnectionStringsSettings ConnectionStrings { get; init; } = null!;
    public JwtConfigSettings JwtConfig { get; init; } = null!;
    public RequestConfigSettings RequestConfig { get; init; } = null!;
}