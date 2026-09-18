using OperationMatrix.Infrastructure.Postgres;

namespace OperationMatrix.Features.Products;

public class CreateProductHandler
{
    private readonly AppDbContext _dbContext;

    public CreateProductHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductResponse> HandleAsync(CreateProductRequest request, CancellationToken ct)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        await _dbContext.Products.AddAsync(product, ct);
        await _dbContext.SaveChangesAsync(ct);

        return new ProductResponse(product.Id, product.Name, product.Description, product.Price);
    }
}