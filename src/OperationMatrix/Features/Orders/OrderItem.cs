using OperationMatrix.Features.Products;

namespace OperationMatrix.Features.Orders;

public class OrderItem
{
    public Guid Id { get; set; }
    
    public Guid OrderId { get; set; }
    
    public Guid ProductId { get; set; }

    public int Quantity { get; set; } = 1;

    public required decimal Price { get; set; }

    public required Order Order { get; set; }
    
    public Product? Product { get; set; }
}