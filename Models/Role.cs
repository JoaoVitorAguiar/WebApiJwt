using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiJwt.Models;

[Table("Roles")]
public class Role
{
    public int Id { get; set; }

    [Column(TypeName = "VARCHAR(80)")]
    public string Name { get; set; }

    public IList<User> Users { get; set; }
}
