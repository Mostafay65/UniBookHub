namespace UniBookHub.Models;

public class College
{
    public int ID { get; set; }
    public string Name { get; set; }
    public IEnumerable<ApplicationUser>? Students { get; set; } = new List<ApplicationUser>();
    public IEnumerable<Subject> Subjects { get; set; } = new List<Subject>();
}