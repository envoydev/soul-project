namespace SoulProject.Domain.Entities;

public sealed class Skill : BaseUserOwnerEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; } = string.Empty;
}