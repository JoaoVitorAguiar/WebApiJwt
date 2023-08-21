using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.Account;

public class UserResponseViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress] // Valida se é e-mail
    public string Email { get; set; }
}
