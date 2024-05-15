﻿using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> Get(string id)
        {
            var basket = await _basketService.GetBasketAsync(id);
            return basket is null ? NotFound(new ApiResponse(404, $"basket with id = {id} not found")) : Ok(basket);

        }

        [HttpDelete]
        public async Task<ActionResult> Delete(string id) => Ok(await _basketService.DeletBasketAsync(id));
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Update(BasketDTO basketDTO)
        {
            var basket = await _basketService.UpdateBasketAsync(basketDTO);
            return basket is null ? NotFound(new ApiResponse(404, $"basket with id = {basketDTO.Id} not found")) : Ok(basket);

        }
    }
}
