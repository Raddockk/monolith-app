using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("transaction");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Type).HasColumnName("type").HasMaxLength(7).IsRequired();
		builder.Property(d => d.TransactionDate).HasColumnName("transaction_date").IsRequired();
		builder.Property(d => d.AccountId).HasColumnName("account_id").IsRequired();
		builder.Property(d => d.CategoryId).HasColumnName("category_id");
		builder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
        
                    
        builder.HasOne(d => d.Account)
                .WithMany(e => e.Transactions)
                .HasForeignKey(d => d.AccountId);

		builder.HasOne(d => d.Category)
                .WithMany(e => e.Transactions)
                .HasForeignKey(d => d.CategoryId);

		builder.HasOne(d => d.User)
                .WithMany(e => e.Transactions)
                .HasForeignKey(d => d.UserId);    
        
    }
}