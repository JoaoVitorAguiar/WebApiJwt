using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.Account;

public class UserLoginViewModel
{
    [Required]
    [EmailAddress] // Valida se é e-mail
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
