using Microsoft.AspNetCore.Identity;

namespace UniBookHub.Models;

public class ApplicationUser : IdentityUser
{
    public string Name { get; set; }
    public string Password { get; set; }
    public bool Accessed { get; set; }
    public int? CollegeId { get; set; }
    public College? College { get; set; }
    public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
}