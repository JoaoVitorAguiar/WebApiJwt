using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Categories")]
public class Category
{
    public Category(string name)
    {
        Name = name;
        Products = new List<Product>();
    }

    [Key]
    public int Id { get; set; }
    public string Name { get; set; }

    public IList<Product> Products { get; set; }
}
