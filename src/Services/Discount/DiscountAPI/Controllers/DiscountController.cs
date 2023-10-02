using AutoMapper;
using DiscountAPI.Dtos;
using DiscountAPI.Entities;
using DiscountAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiscountAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService discountService;
        private readonly IMapper mapper;

        public DiscountController(IDiscountService discountService, IMapper mapper)
        {
            this.discountService = discountService;
            this.mapper = mapper;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDiscount product)
        {
            var productModel = mapper.Map<Product>(product);
            var status = await discountService.AddProductAsync(product);
            return Ok(status);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateDiscount(DiscountUpdateDto update)
        {
            var status = await discountService.UpdateProductDiscountAsync(update);
            return Ok(status);
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<List<ProductDiscount>>> GetProducts()
        {
            var products = await discountService.GetProductDiscountsAsync();
            return Ok(products);
        }
    }
}
