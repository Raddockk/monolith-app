using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class DebtMap : IEntityTypeConfiguration<Debt>
{
    public void Configure(EntityTypeBuilder<Debt> builder)
    {
        builder.ToTable("debt");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
        builder.Property(d => d.Name).HasColumnName("name");
        builder.Property(d => d.Amount).HasColumnName("amount");
        builder.Property(d => d.Currency).HasColumnName("currency").HasMaxLength(3);
        builder.Property(d => d.DueDate).HasColumnName("due_date");
        builder.Property(d => d.AccountId).HasColumnName("account_id");
        builder.Property(d => d.CategoryId).HasColumnName("category_id");
        builder.Property(d => d.UserId).HasColumnName("user_id");


        builder.HasOne(d => d.User)
                .WithMany(e => e.Debts)
                .HasForeignKey(d => d.UserId);

        builder.HasOne(d => d.Account)
                .WithMany(e => e.Debts)
                .HasForeignKey(d => d.AccountId);

        builder.HasOne(d => d.Category)
                .WithMany(e => e.Debts)
                .HasForeignKey(d => d.CategoryId);
    }
}