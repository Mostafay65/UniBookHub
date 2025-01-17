using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniBookHub.Models;

namespace UniBookHub.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(50);
        builder.Property(c => c.Code).HasColumnName("Code").HasMaxLength(10);
        builder.Property(c => c.Book).HasColumnName("Book").HasDefaultValue("Book.pdf");
        builder.Property(c => c.Description).HasColumnName("Description");
        
        builder.HasOne(x => x.College)
            .WithMany(c => c.Courses)
            .HasForeignKey(x => x.CollegeId).IsRequired(true).OnDelete(DeleteBehavior.Cascade);
    }
}