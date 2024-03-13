using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace UniBookHub.Models;

public class College
{
    public int ID { get; set; }
    [Display(Name = "Collage Name")]
    public string Name { get; set; }
    
    [Display(Name = "Number of levels")]
    [Range(4, 10)]
    public int NumberOfLevels { get; set; }
    public IEnumerable<ApplicationUser>? Students { get; set; } = new List<ApplicationUser>();
    public IEnumerable<Course> Courses { get; set; } = new List<Course>();
}