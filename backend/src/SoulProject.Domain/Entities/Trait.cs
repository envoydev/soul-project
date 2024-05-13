namespace SoulProject.Domain.Entities;

public class Trait : BaseUserOwnerEntity
{
    public string Name { get; set; } = null!;
    public TraitSide Side { get; set; }
}