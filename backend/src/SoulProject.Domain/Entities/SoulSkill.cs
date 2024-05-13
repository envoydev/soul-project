namespace SoulProject.Domain.Entities;

public class SoulSkill
{
    public Guid SoulId { get; set; }
    public Soul Soul { get; set; }
    public Guid SkillId { get; set; }
    public Skill Skill { get; set; }
    public SkillLevel Level { get; set; }
}