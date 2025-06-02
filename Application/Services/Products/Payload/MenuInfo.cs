namespace Application.Services.Products.Payload
{
    public class MenuInfo
    {
        public List<ProductInfo> Burgers { get; set; } = new List<ProductInfo>();
        public List<ProductInfo> Beverages { get; set; } = new List<ProductInfo>();
        public List<ProductInfo> Desserts { get; set; } = new List<ProductInfo>();
    }
}
