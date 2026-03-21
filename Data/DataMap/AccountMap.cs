using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class AccountMap : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("account");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
		builder.Property(d => d.Currency).HasColumnName("currency").HasMaxLength(3).IsRequired();
		builder.Property(d => d.BankId).HasColumnName("bank_id");
		builder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
        
                    
        builder.HasOne(d => d.Bank)
                .WithMany(e => e.Accounts)
                .HasForeignKey(d => d.BankId);

		builder.HasOne(d => d.User)
                .WithMany(e => e.Accounts)
                .HasForeignKey(d => d.UserId);    
        
    }
}