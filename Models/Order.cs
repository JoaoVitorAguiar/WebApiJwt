using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Orders")]
public class Order
{
    [Key]
    public int Id { get; set; }
    [Key]
    public Product Product { get; set; }
    [Key]
    public User User { get; set; }
    [Required]
    public DateTime DateRequest  { get; set; }
}
