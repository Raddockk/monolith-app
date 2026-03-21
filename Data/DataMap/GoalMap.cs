using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class GoalMap : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("goal");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Id).HasColumnName("id").ValueGeneratedOnAdd();
		builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(50).IsRequired();
		builder.Property(d => d.FamilyId).HasColumnName("family_id").IsRequired();
		builder.Property(d => d.CreatedById).HasColumnName("created_by_id").IsRequired();
        
                    
        builder.HasOne(d => d.Family)
                .WithMany(e => e.Goals)
                .HasForeignKey(d => d.FamilyId);

		builder.HasOne(d => d.User)
                .WithMany(e => e.Goals)
                .HasForeignKey(d => d.CreatedById);    
        
    }
}