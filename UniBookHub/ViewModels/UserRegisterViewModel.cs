using System.ComponentModel.DataAnnotations;

namespace UniBookHub.ViewModels;

public class UserRegisterViewModel
{
    public string Name{ get; set; }
    
    public string Username{ get; set; }
    
    [DataType(DataType.Password)]
    // [Compare("ConfirmPassword")]
    public string Password{ get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password")]
    public string ConfirmPassword{ get; set; }

    [Display(Name = "College")]
    public int? CollegeId { get; set; }
}