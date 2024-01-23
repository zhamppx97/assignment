namespace api.Domain;

public interface IBaseEntity
{
    Guid UserId { get; set; }
    DateTime CreateDate { get; set; }
    DateTime UpdateDate { get; set; }
}
