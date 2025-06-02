using Domain.Entities;
using Domain.Enums;

namespace Application.Repository
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(ProductEntity product);
        Task<ProductEntity?> GetProductByIdAsync(Guid id);
        Task<List<ProductEntity>> GetAllProductsAsync();
        Task<bool> UpdateProductAsync(ProductEntity product);
        Task<bool> DeleteProductAsync(Guid id);
        Task<List<ProductEntity>> GetProductsInStockAsync();
        Task<List<ProductEntity>> GetProductsInStockByTypeAsync(ProductType productType);
        Task<bool> DecrementStockAsync(List<ProductEntity> productsToDecrement);
    }
}
