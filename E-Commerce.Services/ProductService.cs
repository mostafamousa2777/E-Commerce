using AutoMapper;
using E_Commerce.Core.DataTransferObjects;
using E_Commerce.Core.Entities;
using E_Commerce.Core.Interfaces.Repositories;
using E_Commerce.Core.Interfaces.Services;
using E_Commerce.Core.Specifications;
using E_Commerce.Repository.Specifications;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    public class ProductService : IProductService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public ProductService(IUnitOfWork unitOfWork , IMapper mapper)
        {
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<PaginatedResultDto<ProductToReturnDTO>> GetAllProductsDTOAsync(ProductSpecificationParameters SpecParameters)
		{
			var spesification = new ProductSpecifications(SpecParameters);
			var products = await _unitOfWork.Repository<Product,int>().GetAllWithSpecAsync(spesification);
			var spesificationcount = new ProductCountWithSpec(SpecParameters);

			var count= await _unitOfWork.Repository<Product, int>().GetProductCountSpecAsync(spesificationcount);
            var mappedProduct=  _mapper.Map<IReadOnlyList<ProductToReturnDTO>>(products);
			return new PaginatedResultDto<ProductToReturnDTO>() {
			Data=mappedProduct,
			PageIndex=SpecParameters.PageIndex,
			PageSize=SpecParameters.PageSize,
			TotalCount= count,
			
			};

        }
		public async Task<ProductToReturnDTO> GetProductDTOAsync(int id)
		{
			var spesification = new ProductSpecifications(id);
			var product = await _unitOfWork.Repository<Product, int>().GetWithSpecAsync(spesification);
			return _mapper.Map<ProductToReturnDTO>(product);
		}

        public async Task<IEnumerable<BrandTypeDTO>> GetAllBrandsDTOAsync()
		{
			var brands = await _unitOfWork.Repository<ProductBrand,int>().GetAllAsync();
			return _mapper.Map<IEnumerable<BrandTypeDTO>>(brands);
		}

		public async Task<IEnumerable<BrandTypeDTO>> GetAllTypesDTOAsync()
		{
			var types = await _unitOfWork.Repository<ProductType, int>().GetAllAsync();
			return _mapper.Map<IEnumerable<BrandTypeDTO>>(types);
		}

	}
}
