using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Products")]
public class Product
{
    public Product(string name, float price)
    { 
        Name = name;
        Price = price;
        Users = new List<User>();
    }

    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(80)")]
    public string Name { get; set; }
    [Required]
    public float Price { get; set; }


    public IList<User> Users { get; set; }
}
