using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Products")]
public class Product
{
    // Preciso remover todos os registros salvos
    public Product(string name, float price)
    { 
        Name = name;
        Price = price;
        Orders = new List<Order>();
    }

    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(80)")]
    public string Name { get; set; }
    [Required]
    public float Price { get; set; }


    public IList<Order> Orders { get; set; }
    public Category Category { get; set; }
}
