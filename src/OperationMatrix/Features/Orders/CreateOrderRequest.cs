namespace OperationMatrix.Features.Orders;

public record CreateOrderRequest (List<CreateOrderItemRequest> Items);