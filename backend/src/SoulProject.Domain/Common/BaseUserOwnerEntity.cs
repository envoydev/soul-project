namespace SoulProject.Domain.Common;

public class BaseUserOwnerEntity : BaseAuditableEntity
{
    public Guid UserId { get; set; }
}