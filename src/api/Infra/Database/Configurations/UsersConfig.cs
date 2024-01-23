using api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Infra;

public class UsersConfig : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        builder
            .HasKey(a => a.UserId);

        builder
            .Property(m => m.Hn)
            .IsRequired();

        builder
            .Property(m => m.FirstName)
            .IsRequired()
            .HasMaxLength(400);

        builder
            .Property(m => m.LastName)
            .IsRequired()
            .HasMaxLength(400);

        builder
            .Property(m => m.PhoneNo)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(m => m.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(m => m.IsActive);

        builder
            .Property(m => m.CreateDate)
            .HasColumnType("datetime");

        builder
            .Property(m => m.CreateBy);

        builder
            .Property(m => m.UpdateDate)
            .HasColumnType("datetime");

        builder
            .Property(m => m.UpdateBy);

        builder
            .ToTable("Users");
    }
}
