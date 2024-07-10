using Microsoft.EntityFrameworkCore;
using MyShopPay.DataAccessLayer.Models;

namespace MyShopPay.DataAccessLayer;

internal sealed class MyShopPayDbContext(
    DbContextOptions<MyShopPayDbContext> options
    ) : DbContext(options)
{
    public DbSet<Payment> Payments { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
