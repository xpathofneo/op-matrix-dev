using Microsoft.EntityFrameworkCore;
using Npgsql;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Products;

public class DeleteProductHandler
{
    private readonly AppDbContext _dbContext;

    public DeleteProductHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDeletionResult> HandleAsync(Guid id, CancellationToken ct)
    {
        try
        {
            var deleted = await _dbContext.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync(ct);

            return deleted > 0 ? ProductDeletionResult.Deleted : ProductDeletionResult.NotFound;
        }
        catch (DbUpdateException ex) when (ex.InnerException is PostgresException { SqlState: "23503" })
        {
            return ProductDeletionResult.HasOrders;
        }
    }
}