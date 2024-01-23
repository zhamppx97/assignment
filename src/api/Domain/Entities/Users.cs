namespace api.Domain
{
    public partial class Users : BaseEntity, IBaseEntity
    {
        public Guid UserId { get; set; }
        public int Hn { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid CreateBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public Guid UpdateBy { get; set; }
    }
}
