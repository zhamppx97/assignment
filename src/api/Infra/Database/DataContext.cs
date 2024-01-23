using api.Domain;
using Microsoft.EntityFrameworkCore;

namespace api.Infra;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    #region DbSet List
    public DbSet<Users> Users { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UsersConfig());
    }
}
