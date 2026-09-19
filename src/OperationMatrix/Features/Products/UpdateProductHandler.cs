using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Products;

public class UpdateProductHandler
{
    private readonly AppDbContext _dbContext;

    public UpdateProductHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> HandleAsync(Guid id, UpdateProductRequest request, CancellationToken ct)
    {
        var product = await _dbContext.Products.FindAsync([id], ct);

        if (product is null)
        {
            return false;
        }
        
        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        
        await  _dbContext.SaveChangesAsync(ct);
        return true;
    }
}