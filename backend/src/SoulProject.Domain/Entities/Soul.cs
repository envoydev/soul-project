namespace SoulProject.Domain.Entities;

public sealed class Soul : BaseUserOwnerEntity
{
    public Guid? TrustCircleId { get; set; }
    public TrustCircle? TrustCircle { get; set; }
    public Guid? OccupationId { get; set; }
    public string? AvatarPath { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = string.Empty;
    public string Nickname { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal IncomeAmount { get; set; }
    public Currency IncomeCurrency { get; set; }
    public DateTime Added { get; set; }
    public DateTime Meet { get; set; }
    public List<SoulTrait> SoulTraits { get; set; } = [];
    public List<SoulSkill> SoulSkills { get; set; } = [];
}