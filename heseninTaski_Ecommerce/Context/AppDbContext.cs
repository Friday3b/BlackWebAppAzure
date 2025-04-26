using heseninTaski_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace heseninTaski_Ecommerce.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasKey(u => u.Id); // Əsas açar təyin edilir
    }

}
