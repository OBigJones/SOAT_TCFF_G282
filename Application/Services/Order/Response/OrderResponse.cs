using Application.Services.Order.Payload;

namespace Application.Services.Order.Response
{
    public class OrderResponse
    {
        public string OrderCode { get; set; }
        public string CustomerName { get; set; }
        public List<ProductBaseResponse> ProductList { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
