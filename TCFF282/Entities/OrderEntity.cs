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
        public List<BurgerEntity> BurgerList { get; set; }
        public List<BeverageEntity> Beverages { get; set; }
        public List<DessertEntity> Desserts { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } // e.g., "Pending", "Completed", "Cancelled"
        public DateTime Expiration { get; init; } = DateTime.UtcNow.AddHours(1); // Order expires in 1 hour

        private static string GenerateOrderCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper(); // Generates a unique 8-character code
        }

        public void CalculateTotalPrice()
        {
            decimal total = 0;
            if (BurgerList != null)
                total += BurgerList.Sum(item => item.Price);

            if (Beverages != null)
                total += Beverages.Sum(item => item.Price);

            if (Desserts != null)
                total += Desserts.Sum(item => item.Price);

            TotalPrice = total;
        }
    }
}
