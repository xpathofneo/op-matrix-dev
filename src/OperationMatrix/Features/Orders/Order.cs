namespace OperationMatrix.Features.Orders;

public class Order
{
   public Guid Id { get; set; }
   
   public string OrderNumber { get; set; } = string.Empty;
   
   public DateTime OrderDate { get; set; } =  DateTime.UtcNow;

   public List<OrderItem> Items { get; set; } = [];
   
   public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
   
}