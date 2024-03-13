namespace UniBookHub.Models;

public class Enrollment
{
    public string StudentId { get; set; }
    public int CourseId { get; set; }
    public ApplicationUser Student { get; set; }
    public Course Course { get; set; }
}