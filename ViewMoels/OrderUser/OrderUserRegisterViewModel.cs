using System.ComponentModel.DataAnnotations;

namespace WebApiJwt.ViewMoels.OrderUser;

public class OrderUserRegisterViewModel
{
    [Required]
    public string ProductName { get; set; }
    [Required]
    public DateTime DateRequest { get; set; }
}
