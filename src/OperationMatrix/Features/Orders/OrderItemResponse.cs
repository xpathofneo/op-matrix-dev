namespace OperationMatrix.Features.Orders;

public record OrderItemResponse (Guid ProductId, int Quantity, decimal Price);