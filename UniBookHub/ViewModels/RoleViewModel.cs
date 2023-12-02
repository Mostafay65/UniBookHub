using System.ComponentModel.DataAnnotations;

namespace UniBookHub.ViewModels;

public class RoleViewModel
{
    [Required]
    [Display(Name = "Role Name")]
    public string Name{ get; set; }
}