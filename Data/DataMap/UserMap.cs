using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Login).HasColumnName("login").HasMaxLength(32).IsRequired();
		builder.Property(d => d.Mail).HasColumnName("mail").HasMaxLength(50);
		builder.Property(d => d.PasswordHash).HasColumnName("password_hash").HasMaxLength(32).IsRequired();
		builder.Property(d => d.Firstname).HasColumnName("firstname").HasMaxLength(50).IsRequired();
		builder.Property(d => d.Lastname).HasColumnName("lastname").HasMaxLength(50).IsRequired();
		builder.Property(d => d.Patronymic).HasColumnName("patronymic").HasMaxLength(50);
		builder.Property(d => d.Age).HasColumnName("age");
		builder.Property(d => d.RoleId).HasColumnName("role_id").IsRequired();
        builder.HasIndex(v => v.Login).IsUnique();
		builder.HasIndex(v => v.Mail).IsUnique();
                    
        builder.HasOne(d => d.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(d => d.RoleId);    
        
    }
}