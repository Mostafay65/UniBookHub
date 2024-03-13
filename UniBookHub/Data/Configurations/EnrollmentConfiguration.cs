using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniBookHub.Models;

namespace UniBookHub.Data.Configurations;

public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        builder.Property(c => c.StudentId).HasColumnName("StudentId");
        builder.Property(c => c.CourseId).HasColumnName("CourseId");
        builder.HasKey(e => new { e.StudentId, e.CourseId });
    }
}