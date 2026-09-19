using Microsoft.AspNetCore.Mvc;

namespace OperationMatrix.Features.Orders;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateOrderRequest itemRequest,
        [FromServices] CreateOrderHandler handler,
        CancellationToken ct)
    {
        var order = await handler.HandleAsync(itemRequest, ct);
        return CreatedAtAction(
            nameof(GetById),
            new { id = order.Id },
            order);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        [FromServices] GetOrderByIdHandler handler,
        CancellationToken ct)
    {
        var order = await handler.HandleAsync(id, ct);

        return order is null ? NotFound() : Ok(order);
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteOrderHandler handler,
        CancellationToken ct)
    {
        var result = await handler.HandleAsync(id, ct);
        
        return result ? NoContent() : NotFound();
    }
}