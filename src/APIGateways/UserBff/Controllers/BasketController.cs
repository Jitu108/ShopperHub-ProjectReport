using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserBff.Dtos;
using UserBff.Services;

namespace UserBff.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {
            this.basketService = basketService;
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartDto>> GetBasket(int userId)
        {
            var basket = await basketService.GetBasket(userId);
            if(basket == null)
            {
                basket = new ShoppingCartDto();
                basket.UserId = userId;
            }
            
            return Ok(basket);
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateBasket(ShoppingCartDto cart)
        {
            return Ok(await basketService.UpdateBasket(cart));
        }

        [HttpDelete("{userId}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBasket(int userId)
        {
            var status = await basketService.DeleteBasket(userId);
            return Ok(status);
        }
    }
}
