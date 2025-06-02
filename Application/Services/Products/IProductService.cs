using Application.Services.Products.Payload;

namespace Application.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductInfo>> GetAllProductsAsync();
        Task<IEnumerable<ProductInfo>> GetAllProductsInStockAsync();
        Task<MenuInfo> GetMenuAsync();
        Task<bool> UpdateProductByIdAsync(ProductPayload productPayload);
    }
}
