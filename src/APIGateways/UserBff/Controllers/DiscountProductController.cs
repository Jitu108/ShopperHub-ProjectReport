using UserBff.Dtos;
using UserBff.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserBff.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountProductController : ControllerBase
    {
        private readonly IDiscountProductService discountService;

        public DiscountProductController(IDiscountProductService discountService)
        {
            this.discountService = discountService;
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
