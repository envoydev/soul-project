namespace SoulProject.Domain.Common;

public class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedDate { get; set; }
}