using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class ProductBase
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public OrderEntity Order { get; set; }  
    }
}
