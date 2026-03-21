using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class BudgetMap : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("budget");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Period).HasColumnName("period").HasMaxLength(10).IsRequired();
		builder.Property(d => d.StartDate).HasColumnName("start_date").IsRequired();
		builder.Property(d => d.EndDate).HasColumnName("end_date");
		builder.Property(d => d.FamilyId).HasColumnName("family_id").IsRequired();
		builder.Property(d => d.CategoryId).HasColumnName("category_id");
		builder.Property(d => d.CreatedById).HasColumnName("created_by_id").IsRequired();
        
                    
        builder.HasOne(d => d.Family)
                .WithMany(e => e.Budgets)
                .HasForeignKey(d => d.FamilyId);

		builder.HasOne(d => d.Category)
                .WithMany(e => e.Budgets)
                .HasForeignKey(d => d.CategoryId);

		builder.HasOne(d => d.User)
                .WithMany(e => e.Budgets)
                .HasForeignKey(d => d.CreatedById);    
        
    }
}