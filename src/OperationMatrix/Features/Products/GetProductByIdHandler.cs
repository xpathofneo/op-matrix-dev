using Microsoft.EntityFrameworkCore;
using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Products;

public class GetProductByIdHandler
{
    private readonly AppDbContext _dbContext;

    public GetProductByIdHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductResponse?> HandleAsync(Guid id, CancellationToken ct)
    {
        return await _dbContext.Products.AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new ProductResponse(p.Id, p.Name, p.Description, p.Price))
            .FirstOrDefaultAsync(ct);
    }
}