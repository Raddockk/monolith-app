using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class FamilyMemberMap : IEntityTypeConfiguration<FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.ToTable("family_member");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.UserId).HasColumnName("user_id").IsRequired();
		builder.Property(d => d.FamilyId).HasColumnName("family_id").IsRequired();
		builder.Property(d => d.JoinDate).HasColumnName("join_date");
        
                    
        builder.HasOne(d => d.User)
                .WithMany(e => e.FamilyMembers)
                .HasForeignKey(d => d.UserId);

		builder.HasOne(d => d.Family)
                .WithMany(e => e.FamilyMembers)
                .HasForeignKey(d => d.FamilyId);    
        
    }
}