using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities.Basket;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace E_Commerce.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepositoriy _basketRepositoriy;
        private readonly IMapper _mapper;


        public BasketService(IBasketRepositoriy basketRepositoriy, IMapper mapper)
        {
            _basketRepositoriy = basketRepositoriy;
            _mapper = mapper;
        }

        public async Task<bool> DeletBasketAsync(string id) => await _basketRepositoriy.DeleteCustomerBasketAsync(id);

        public async Task<BasketDTO?> GetBasketAsync(string id)
        {
         var basket=   await _basketRepositoriy.GetCustomerBasketAsync(id);
            return basket is null ?null: _mapper.Map<BasketDTO?>(basket);
        } 

        public async Task<BasketDTO?> UpdateBasketAsync(BasketDTO basket)
        {
            var customerbasket = _mapper.Map<CustomerBasket>(basket);
            var updatedbasket = await _basketRepositoriy.UpdateCustomerBasketAsync(customerbasket);
            return updatedbasket is null ? null : _mapper.Map<BasketDTO>(updatedbasket);
        }
    }
}
