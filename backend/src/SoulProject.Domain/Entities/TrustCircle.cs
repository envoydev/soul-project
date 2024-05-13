namespace SoulProject.Domain.Entities;

public sealed class TrustCircle : BaseUserOwnerEntity
{
    public string Name { get; set; } = null!;
    public int PositionIndex { get; set; }
    public string Color { get; set; } = string.Empty;
    public string? Description { get; set; }
}