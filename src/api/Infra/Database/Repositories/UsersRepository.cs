using api.Core;
using api.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Infra;

public class UsersRepository(DataContext context) : BaseRepository<Users>(context), IUsersRepository
{
    public async Task<Users> GetByIdAsync(Guid id)
    {
        return await base._context.Users.FirstOrDefaultAsync(u => u.UserId == id);
    }
}
