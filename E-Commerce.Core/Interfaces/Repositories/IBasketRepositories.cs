using E_Commerce.Core.Entities.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Repositories
{
    public interface IBasketRepositoriy
    {
        Task<CustomerBasket?> GetCustomerBasketAsync(string id);
        Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket customerBasket);
        Task<bool> DeleteCustomerBasketAsync(string id);

    }
}
