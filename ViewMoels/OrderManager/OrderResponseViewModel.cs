using System.ComponentModel.DataAnnotations;
using WebApiJwt.Models;

namespace WebApiJwt.ViewMoels.OrderManager;

public class OrderResponseViewModel
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Product { get; set; }
    [Required]
    public string User { get; set; }
    [Required]
    public DateTime DateRequest { get; set; }
}
