namespace UniBookHub.Models;

public class Subject
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Book { get; set; }
    public string? Description { get; set; }
    public int CollegeId { get; set; }
    public College College { get; set; }
    public IEnumerable<ApplicationUser> Students { get; set; } = new List<ApplicationUser>();
}