using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class ProductEntity
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public ProductType Type { get; set; }

        [ForeignKey("OrderId")]
        public long? OrderId { get; set; }

        public OrderEntity Order { get; set; }
    }
}
