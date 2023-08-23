using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Orders")]
public class Order
{
    public Order(Product product, User user, DateTime dateRequest)
    {
        Product = product;
        User = user;
        DateRequest = dateRequest;
    }

    [Key]
    public int Id { get; set; }
    [Key]
    public Product Product { get; set; }
    [Key]
    public User User { get; set; }
    public DateTime DateRequest  { get; set; }
}
