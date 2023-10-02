using AutoMapper;
using Basket.API.Data;
using Basket.API.Data.Entities;
using Basket.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IShoppingCartRepo repo;
        private readonly IMapper mapper;

        public BasketController(IShoppingCartRepo repo, IMapper mapper)
        {
            this.repo = repo;
            this.mapper = mapper;
        }

        [HttpGet("{userId}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(int userId)
        {
            var basket = await repo.GetBasket(userId);
            return Ok(basket ?? new ShoppingCart(userId));
        }

        [HttpPost(Name = "UpdateBasket")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<bool>> UpdateBasket([FromBody] ShoppingCart cart)
        {
            var status = await repo.UpdateBasket(cart);
            return Ok(status);
        }

        [HttpDelete("{userId}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBasket(int userId)
        {
            var status = await repo.DeleteBasket(userId);
            return Ok(status);
        }

    }
}
