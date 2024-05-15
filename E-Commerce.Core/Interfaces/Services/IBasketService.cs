using E_Commerce.Core.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Services
{
    public interface IBasketService
    {
        Task<BasketDTO?>GetBasketAsync(String id);
        Task<BasketDTO?> UpdateBasketAsync(BasketDTO  basket);
        Task<bool> DeletBasketAsync(String id);


    }
}
