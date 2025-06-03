using Application.Services.Products.Payload;
using Domain.Entities;

namespace Application.Services.Products.Mappers
{
    public static class ProductMappers
    {
        public static ProductInfo ToProductInfo(this ProductEntity product)
        {
            if (product == null) throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            return new ProductInfo
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                StockQuantity = product.Quantity,
                Category = product.Type
            };
        }

        public static IEnumerable<ProductInfo> ToProductInfoList(this IEnumerable<ProductEntity> products)
        {
            if (products == null || !products.Any()) return Enumerable.Empty<ProductInfo>();
            return products.Select(p => p.ToProductInfo());
        }

        public static MenuInfo ToMenuInfo(this IEnumerable<ProductEntity> products)
        {
            if (products == null || !products.Any()) return new MenuInfo();
            var menu = new MenuInfo
            {
                Burgers = products.Where(p => p.Type == Domain.Enums.ProductType.Burger).ToProductInfoList().ToList(),
                Beverages = products.Where(p => p.Type == Domain.Enums.ProductType.Beverage).ToProductInfoList().ToList(),
                Desserts = products.Where(p => p.Type == Domain.Enums.ProductType.Dessert).ToProductInfoList().ToList()
            };
            return menu;
        }

        public static ProductEntity ToEntity(this ProductPayload productPayload)
        {
            if (productPayload == null) throw new ArgumentNullException(nameof(productPayload), "Product payload cannot be null.");
            return new ProductEntity
            {
                Id = productPayload.Id,
                Name = productPayload.Name,
                Description = productPayload.Description,
                Price = productPayload.Price,
                Quantity = productPayload.Stock,
                Type = productPayload.Type
            };
        }
    }
}
