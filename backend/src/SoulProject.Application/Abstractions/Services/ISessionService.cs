namespace SoulProject.Application.Abstractions.Services;

public interface ISessionService
{
    Guid? UserId { get; }
    Role? UserRole { get; }
    public DateTime? TokenExpiration { get; }
}