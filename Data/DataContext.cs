using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebApiJwt.Models;

namespace WebApiJwt.Data;

public class DataContext : DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DataContext(DbContextOptions options) : base(options)
    {
        
    }
}
