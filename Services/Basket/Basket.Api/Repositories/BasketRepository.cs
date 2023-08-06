using Basket.Api.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.Api.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart> GetUserBasket(string userName)
        {
            var basket = await this._redisCache.GetStringAsync(userName);
            return string.IsNullOrEmpty(basket) ? null : JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await this._redisCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await this.GetUserBasket(basket.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await this._redisCache.RemoveAsync(userName);
        }
    }
}
