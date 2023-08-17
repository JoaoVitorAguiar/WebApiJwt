using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace WebApiJwt.Models;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public IList<Role> Roles { get; set; }
}
