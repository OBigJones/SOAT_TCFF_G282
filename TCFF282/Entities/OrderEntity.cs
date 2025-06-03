using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Domain.Entities
{
    [Table("orders")]
    public class OrderEntity
    {
        [Key]
        public long Id { get; init; }
        public string OrderCode { get; init; } = GenerateOrderCode();
        public UserEntity? User { get; set; }
        public List<OrderItemEntity> OrderItems { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Expiration { get; init; } = DateTime.UtcNow.AddHours(1); // Order expires in 1 hour

        private static string GenerateOrderCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Generates a unique 8-character code
        }

        public void CalculateTotalPrice()
        {
            decimal total = 0;
            total += OrderItems.Sum(item => item.Product.Price);

            TotalPrice = total;
        }
    }
}
