using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.Account;

public class UserRegisterViewModel : UserResponseViewModel
{
    
    [Required]
    public string Password { get; set; }
}
