namespace api.Domain;

public class BaseEntity : IBaseEntity
{
    public Guid UserId { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
