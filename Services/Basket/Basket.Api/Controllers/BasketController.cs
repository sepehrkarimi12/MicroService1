using System.Net;
using Basket.Api.Entities;
using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var basket = await this._basketRepository.GetUserBasket(userName);
            return Ok(basket ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            //todo: get data from discount.grpc and calculate final price of product
            foreach (var item in basket.Items)
            {
                var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            return Ok(await _basketRepository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _basketRepository.DeleteBasket(userName);
            return Ok();
        }
    }
}
