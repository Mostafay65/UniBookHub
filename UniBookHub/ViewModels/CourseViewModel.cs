using System.ComponentModel.DataAnnotations;

namespace UniBookHub.ViewModels;

public class CourseViewModel
{
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(10)]
    public string Code { get; set; }
    [Display(Name = "Upload Book PDF")]
    // [Required(ErrorMessage = "Please upload a book")]
    public IFormFile? Book { get; set; }
    public string Description { get; set; }

    [Range(1, 100, ErrorMessage = "Please select a specific level")]
    [Display(Name = "Level")]
    public int LevelNumber { get; set; }
    public int CollegeId { get; set; }
}