namespace OperationMatrix.Features.Products;

public record CreateProductRequest (string Name, string Description, decimal Price);