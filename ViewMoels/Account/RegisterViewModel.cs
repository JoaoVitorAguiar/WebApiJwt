using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.Account;

public class RegisterViewModel
{
    [Required]
    public string Name { get; set; }

    [Required]
    [EmailAddress] // Valida se é e-mail
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}
