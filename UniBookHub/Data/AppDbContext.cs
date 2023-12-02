using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UniBookHub.Models;

namespace UniBookHub.Data;


public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<College> Colleges { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public AppDbContext()
    {
        
    }
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetConnectionString("CS"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}