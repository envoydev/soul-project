namespace SoulProject.Domain.Entities;

public sealed class User : BaseAuditableEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string UserName { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiration { get; set; }
}