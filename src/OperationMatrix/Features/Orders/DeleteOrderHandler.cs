using Microsoft.EntityFrameworkCore;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Orders;

public class DeleteOrderHandler
{
    private readonly AppDbContext _dbContext;

    public DeleteOrderHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(Guid id, CancellationToken ct)
    {
        var deleted = await _dbContext.Orders
            .Where(o => o.Id == id)
            .ExecuteDeleteAsync(ct);

        return deleted > 0;
    }
}