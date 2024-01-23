using api.Domain;
using System.ComponentModel.DataAnnotations;

namespace api.Core;

public interface IUsersService
{
    Task<UserServiceResponse<List<UsersResponse>>> SelectAsync();
    Task<UserServiceResponse<Users>> InsertAsync(UserServiceRequest req);
    Task<UserServiceResponse<Users>> UpdateAsync(UserServiceRequest req);
    Task<UserServiceResponse<Users>> DeleteAsync(Guid userId);
}

public class UserServiceRequest
{
    public Guid UserId { get; set; }
    [Required]
    public int Hn { get; set; }
    [Required]
    public string FirstName { get; set; } = null!;
    [Required]
    public string LastName { get; set; } = null!;
    [Required]
    [StringLength(10, MinimumLength = 10)]
    public string PhoneNo { get; set; } = null!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
}

public class UserServiceResponse<T> where T : class
{
    public Int32 TimeStamp { get; set; } = (int)DateTime.UtcNow.Subtract(DateTime.UnixEpoch).TotalSeconds;
    public int Code { get; set; } = 2000;
    public string Messages { get; set; } = "success";
    public T? Data { get; set; }
}

public class UsersResponse
{
    public Guid UserId { get; set; }
    public string Hn { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreateDate { get; set; }
    public Guid CreateBy { get; set; }
    public DateTime UpdateDate { get; set; }
    public Guid UpdateBy { get; set; }
}
