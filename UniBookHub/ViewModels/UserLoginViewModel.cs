using System.ComponentModel.DataAnnotations;

namespace UniBookHub.ViewModels;

public class UserLoginViewModel
{
    public string UserName{ get; set; }
    
    [DataType(DataType.Password)]
    public string Password{ get; set; }
    
    public bool RememberMe{ get; set; }
}