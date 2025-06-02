using Application.Repository;
using Application.Services.Products.Mappers;
using Application.Services.Products.Payload;

namespace Application.Services.Products
{
    internal class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductInfo>> GetAllProductsAsync()
        {
            var allProducts = await _productRepository.GetAllProductsAsync();

            return ProductMappers.ToProductInfoList(allProducts);
        }

        public async Task<IEnumerable<ProductInfo>> GetAllProductsInStockAsync()
        {
            var productsInStock = await _productRepository.GetProductsInStockAsync();
            return ProductMappers.ToProductInfoList(productsInStock);
        }

        public async Task<MenuInfo> GetMenuAsync()
        {
            var products = await _productRepository.GetAllProductsAsync();
            return products.ToMenuInfo();
        }

        public async Task<bool> UpdateProductByIdAsync(ProductPayload productPayload)
        {
            if (productPayload == null)
            {
                throw new ArgumentNullException(nameof(productPayload), "Product details cannot be null.");
            }
            var productEntity = ProductMappers.ToEntity(productPayload);
            return await _productRepository.UpdateProductAsync(productEntity);
        }
    }
}
