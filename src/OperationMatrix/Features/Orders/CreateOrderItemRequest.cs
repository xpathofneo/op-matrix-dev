namespace OperationMatrix.Features.Orders;

public record CreateOrderItemRequest (Guid ProductId, int Quantity);