using Application.Services.Products;
using Application.Services.Products.Payload;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProductsAsync();
            if (products == null || !products.Result.Any())
            {
                return NotFound("No products found.");
            }
            return Ok(products);
        }

        [HttpGet("GetAllProductsInStock")]
        public IActionResult GetAllProductsWithStock()
        {
            var products = _productService.GetAllProductsInStockAsync();
            if (products == null || !products.Result.Any())
            {
                return NotFound("No products found with stock information.");
            }
            return Ok(products);
        }

        [HttpPost("UpdateProductById")]
        public IActionResult UpdateProductById([FromBody] ProductPayload productPayload)
        {
            if (productPayload == null)
            {
                return BadRequest("Product details cannot be null.");
            }
            var result = _productService.UpdateProductByIdAsync(productPayload);
            if (!result.Result)
            {
                return NotFound("Product not found or update failed.");
            }
            return Ok("Product updated successfully.");
        }

        [HttpGet("GetMenu")]
        public IActionResult GetMenu()
        {
            var menu = _productService.GetMenuAsync();
            if (menu == null)
            {
                return NotFound("Menu not found.");
            }
            return Ok(menu);
        }
    }
}
