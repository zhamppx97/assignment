using api.Domain;

namespace api.Core;

public interface IUsersRepository : IBaseRepository<Users>
{
    public Task<List<Users>> GetAllAsync();
    public Task<Users> GetByIdAsync(Guid id);
}
