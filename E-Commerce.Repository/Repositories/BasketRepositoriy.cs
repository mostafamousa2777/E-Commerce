using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Repositories
{
    public class BasketRepositoriy : IBasketRepositoriy
    {
        private readonly IDatabase _database;
        public BasketRepositoriy(IConnectionMultiplexer connection)
        {
            _database=connection.GetDatabase();
        }
        public async Task<bool> DeleteCustomerBasketAsync(string id) => await _database.KeyDeleteAsync(id);

        public async Task<CustomerBasket?> GetCustomerBasketAsync(string id)
        {
            var basket=await _database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket)
        {
            var serilized=JsonSerializer.Serialize(customerBasket);
           var result=await _database.StringSetAsync(customerBasket.Id, serilized,TimeSpan.FromDays(7));
            return result ? await GetCustomerBasketAsync(customerBasket.Id) : null;
        }
    }
}
