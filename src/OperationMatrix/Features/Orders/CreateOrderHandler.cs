using Microsoft.EntityFrameworkCore;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Orders;

public class CreateOrderHandler
{
    private readonly AppDbContext _dbContext;

    public CreateOrderHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderResponse> HandleAsync(CreateOrderRequest request, CancellationToken ct)
    {
         if (request.Items is null || request.Items.Count == 0)                                                                                                                                                                           
             throw new BadHttpRequestException("Order must contain at least one item.");                                                                                                                                                                              
         
         var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();                                                                                                                                                                                 
         var products = await _dbContext.Products                                                                                                                                                                                                                             
             .Where(p => productIds.Contains(p.Id))                                                                                                                                                                                                                   
             .ToDictionaryAsync(p => p.Id, ct);                                                                                                                                                                                                                       
                                                                                                                                                                                                                                                                      
         if (products.Count != productIds.Count)                                                                                                                                                                                                                      
             throw new BadHttpRequestException("Some products not found.");                                                                                                                                                                                           
                                                                                                                                                                                                                                                                      
         var order = new Order                                                                                                                                                                                                                                        
         {                                                                                                                                                                                                                                                            
             Id = Guid.NewGuid(),                                                                                                                                                                                                                                     
             OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid():N}"[..20],                                                                                                                                                               
             OrderDate = DateTime.UtcNow,                                                                                                                                                                                                                             
             Items = request.Items.Select(i => new OrderItem                                                                                                                                                                                                          
             {                                                                                                                                                                                                                                                        
                 Id = Guid.NewGuid(),                                                                                                                                                                                                                                 
                 ProductId = i.ProductId,                                                                                                                                                                                                                             
                 Quantity = i.Quantity,                                                                                                                                                                                                           
                 Price = products[i.ProductId].Price,                                                                                                                                                                                      
                 Order = null!                                                                                                                                                                                    
             }).ToList()                                                                                                                                                                                                                                              
         };                                                                                                                                                                                                                                                           
                                                                                                                                                                                                                                                                      
         await _dbContext.Orders.AddAsync(order, ct);                                                                                                                                                                                                                         
         await _dbContext.SaveChangesAsync(ct);                                                                                                                                                                                                                               
                                                                                                                                                                                                                                                                      
         return new OrderResponse(                                                                                                                                                                                                                                        
             order.Id,                                                                                                                                                                                                                                                    
             order.OrderNumber,                                                                                                                                                                                                                                           
             order.OrderDate,                                                                                                                                                                                                                                             
             order.Items.Select(i => new OrderItemResponse(i.ProductId, i.Quantity, i.Price)).ToList(),                                                                                                                                                                   
             order.Items.Sum(i => i.Price * i.Quantity)                                                                                                                                                                   
         ); 
    }
}