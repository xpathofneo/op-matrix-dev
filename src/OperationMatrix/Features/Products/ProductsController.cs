using Microsoft.AspNetCore.Mvc;

namespace OperationMatrix.Features.Products;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateProductRequest request,
        [FromServices] CreateProductHandler handler,
        CancellationToken ct)
    {
        var product = await handler.HandleAsync(request, ct);
        return CreatedAtAction(
            nameof(GetById),
            new { id =  product.Id },
            product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetProductByIdHandler handler,
        CancellationToken ct)
    {
        var product = await handler.HandleAsync(id, ct);

        return product is null ? NotFound() : Ok(product);
    }
    
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateProductRequest request,
        [FromServices] UpdateProductHandler handler,
        CancellationToken ct)
    {
            var ok = await handler.HandleAsync(id, request, ct);
            
            return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteProductHandler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(id, ct);

        return result switch
        {
            ProductDeletionResult.Deleted => NoContent(),
            ProductDeletionResult.NotFound => NotFound(),
            ProductDeletionResult.HasOrders => Conflict("Product is used in orders"),
            _ => Problem(
                title: "Unknown deletion result",
                statusCode: StatusCodes.Status500InternalServerError)
        };
    }
}