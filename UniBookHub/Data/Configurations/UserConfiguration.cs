using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniBookHub.Models;

namespace UniBookHub.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        builder.Property(c => c.Name).HasColumnName("Name");
        builder.Property(c => c.Password).HasColumnName("Password");
        builder.Property(p=>p.Accessed).HasDefaultValue(false);

        builder.HasMany(x => x.Courses)
            .WithMany(x => x.Students)
            .UsingEntity<Enrollment>();
        
        builder.HasOne(x => x.College)
            .WithMany(x => x.Students)
            .HasForeignKey(x => x.CollegeId)
            .IsRequired(false).OnDelete(DeleteBehavior.Cascade);
    }
}