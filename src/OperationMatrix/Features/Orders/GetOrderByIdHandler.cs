using Microsoft.EntityFrameworkCore;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Orders;

public class GetOrderByIdHandler
{
    private readonly AppDbContext _dbContext;

    public GetOrderByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<OrderResponse?> HandleAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Orders.AsNoTracking()
            .Where(o => o.Id == id)
            .Select(o => new OrderResponse(
                o.Id,
                o.OrderNumber,
                o.OrderDate,
                o.Items.Select(i => new OrderItemResponse(i.ProductId, i.Quantity, i.Price)).ToList(),
                o.Items.Sum(i => i.Price * i.Quantity)
            ))
            .FirstOrDefaultAsync(ct);
    }
}