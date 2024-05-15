using E_Commerce.Core.Entities;
using E_Commerce.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
    public class ProductSpecifications : BaseSpecifications<Product>
	{
		// This ctor is for retrieving ONLY ONE PRODUCT based on the id .....
		public ProductSpecifications(int id) : base(product => product.Id == id)
		{
			// The includes that must be done in any ctor !!
			IncludeExpressions.Add(product => product.ProductBrand);
			IncludeExpressions.Add(product => product.ProductType);
		}


		// This ctor is for retrieving PRODUCTS BASED ON SOME CRITERIA (in our case the criteria can be brand , type) .....
		public ProductSpecifications(ProductSpecificationParameters specs) 
			: base(
				 product => 
					  (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
					  &&
					  (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
                  &&
        (string.IsNullOrEmpty(specs.SearchValue)||product.Name.ToLower().Contains(specs.SearchValue)
				 
				 )
                  )
		{
			// To avoid having many parameters in the ctor , we put them all in a class "ProductSpecificationParameters"
			// and use an object from it now


			// The includes that must be done in any ctor !!
			IncludeExpressions.Add(product => product.ProductBrand);
			IncludeExpressions.Add(product => product.ProductType);
			//Pagenation
			ApplyPagenated(specs.PageSize, specs.PageIndex);


			
			//sort
			if (specs.Sort is not null) {
                switch (specs.Sort)
                {
					case ProductSortingParameters.NameAsc:
						OrderBy=x=>x.Name; 
						break;
                    case ProductSortingParameters.NameDesc:
                        OrderByDesc = x => x.Name;
                        break;
                    case ProductSortingParameters.PriceAsc:
                        OrderBy = x => x.Price;
                        break;
                        case ProductSortingParameters.PriceDesc:
                        OrderByDesc = x => x.Price;
                        break;

                    default:
                        OrderBy = x => x.Name;
                        break;
                }

            }
			else
			{
                OrderBy = x => x.Name;
            }



        }
    }
}
