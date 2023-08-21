using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApiJwt.Models;

[Table("Users")]
public class User
{
    public User(string name, string email, string passwordHash)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Roles = new List<Role>();
        Orders = new List<Order>();
    }

    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(80)")]
    public string Name { get; set; }

    [Column(TypeName = "VARCHAR(80)")]
    public string Email { get; set; }

    [Column(TypeName = "VARCHAR(255)")]
    public string PasswordHash { get; set; }

    public IList<Role> Roles { get; set; }
    public IList<Order> Orders { get; set; }
}
