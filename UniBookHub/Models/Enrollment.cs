namespace UniBookHub.Models;

public class Enrollment
{
    public string StudentId { get; set; }
    public int SubjectId { get; set; }
    public ApplicationUser Student { get; set; }
    public Subject Subject { get; set; }
}