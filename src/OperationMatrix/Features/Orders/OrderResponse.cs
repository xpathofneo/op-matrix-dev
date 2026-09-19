namespace OperationMatrix.Features.Orders;

public record OrderResponse (Guid Id, string OrderNumber, DateTime OrderDate, List<OrderItemResponse> Items, decimal TotalPrice);