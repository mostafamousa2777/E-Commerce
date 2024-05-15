using E_Commerce.API.Errors;
using E_Commerce.API.Helper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace E_Commerce.API.Controllers
{
    [Route("api/[controller]")]      // or [Route("api/[controller]/[Action]")]    --> which will use the action name       
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}

		[HttpGet]
        [Cash(45)]
		[Authorize]

        public async Task<ActionResult<IEnumerable<ProductToReturnDTO>>> GetProducts([FromQuery]ProductSpecificationParameters SpecParameters)
		{
			return Ok(await _productService.GetAllProductsDTOAsync(SpecParameters));
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int id)

		{
			//throw new Exception();
			
			var product = await _productService.GetProductDTOAsync(id);

            return product is not null ? Ok(product) : NotFound(new ApiResponse(404,$"Product with id = {id} is not found"));
        }

		[HttpGet("brands")]
		public async Task<ActionResult<IEnumerable<BrandTypeDTO>>> GetBrands()
		{
			return Ok(await _productService.GetAllBrandsDTOAsync());
		}

		[HttpGet("types")]
		public async Task<ActionResult<IEnumerable<BrandTypeDTO>>> GetTypes()
		{
			return Ok(await _productService.GetAllTypesDTOAsync());
		}
	}
}
