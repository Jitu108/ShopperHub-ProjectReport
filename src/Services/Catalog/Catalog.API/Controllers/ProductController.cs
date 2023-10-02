using AutoMapper;
using Catalog.API.Dtos;
using Catalog.API.Entities;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper
            )
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet("ById/{productId}", Name = "GetProductById")]
        public async Task<ActionResult<ProductRead>> GetProductById(int productId)
        {
            var product = await productService.GetProductByIdAsync(productId);
            var productDto = mapper.Map<ProductRead>(product);
            return Ok(productDto);
        }

        [HttpGet("ByBrand/{brandId}", Name = "GetProductByBrandId")]
        public async Task<ActionResult<IEnumerable<ProductRead>>> GetProductByBrandId(int brandId)
        {
            var products = await productService.GetProductByBrandIdAsync(brandId);
            return Ok(products);
        }

        [HttpGet("ByType/{typeId}", Name = "GetProductByTypeId")]
        public async Task<ActionResult<IEnumerable<ProductRead>>> GetProductByTypeId(int typeId)
        {
            var products = await productService.GetProductByCatalogTypeIdAsync(typeId);
            return Ok(products);
        }

        [HttpGet("GetAll", Name = "GetAllProducts")]
        public async Task<ActionResult<IEnumerable<ProductRead>>> GetAllProducts()
        {
            var products = await productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost("Add", Name = "AddProduct")]
        public async Task<ActionResult<bool>> AddProduct(ProductCreate product)
        {
            
            var status = await productService.AddProductAsync(product);
            return Ok(status);
        }

        [HttpPut("Update/{productId}", Name = "UpdateProduct")]
        public async Task<ActionResult<bool>> UpdateProduct(int productId, [FromBody] ProductCreate product)
        {
            product.Id = productId;
            var status = await productService.UpdateProductAsync(product);
            return Ok(status);
        }

        [HttpDelete("Delete/{productId}", Name = "DeleteProductById")]
        public async Task<ActionResult<bool>> DeleteProductById(int productId)
        {
            var status = await productService.DeleteProductAsync(productId);
            return Ok(status);
        }

    }
}
