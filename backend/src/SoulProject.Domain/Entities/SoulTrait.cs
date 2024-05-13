namespace SoulProject.Domain.Entities;

public class SoulTrait
{
    public Guid SoulId { get; set; }
    public Soul Soul { get; set; }
    public Guid TraitId { get; set; }
    public Trait Trait { get; set; }
}