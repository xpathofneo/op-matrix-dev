using Microsoft.EntityFrameworkCore;
using OperationMatrix.Features.Orders;
using OperationMatrix.Features.Products;

namespace OperationMatrix.Infrastructure.Postgres;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Order> Orders { get; set; }
    
    public DbSet<OrderItem> OrderItems { get; set; }
    
    public DbSet<Product>  Products { get; set; }
    
}