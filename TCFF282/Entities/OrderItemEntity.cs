using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

[Table("order_items")]
public class OrderItemEntity
{
    [Key]
    public long Id { get; set; }
    
    public OrderEntity Order { get; set; }
    
    [ForeignKey("OrderId")]
    public long OrderId { get; set; }
    
    public ProductEntity Product { get; set; }
    
    [ForeignKey("ProductId")]
    public long ProductId { get; set; }
}