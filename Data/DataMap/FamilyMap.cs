using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Global;
public class FamilyMap : IEntityTypeConfiguration<Family>
{
    public void Configure(EntityTypeBuilder<Family> builder)
    {
        builder.ToTable("family");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.Name).HasColumnName("name").HasMaxLength(30).IsRequired();
        
                    
            
        
    }
}