namespace SoulProject.Application.CQRS.Users.Dtos;

public class UserDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Role Role { get; set; }
}