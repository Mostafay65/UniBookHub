using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniBookHub.Models;

namespace UniBookHub.Data.Configurations;

public class CollegeConfiguration : IEntityTypeConfiguration<College>
{
    public void Configure(EntityTypeBuilder<College> builder)
    {
        builder.ToTable("Colleges");
        builder.HasKey(c => c.ID);
        builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(50);
    }
}