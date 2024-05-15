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
    public class ProductCountWithSpec : BaseSpecifications<Product>
    {
        public ProductCountWithSpec(ProductSpecificationParameters specs) : base(
                 product =>
                      (!specs.TypeId.HasValue || product.TypeId == specs.TypeId.Value)
                      &&
                      (!specs.BrandId.HasValue || product.BrandId == specs.BrandId.Value)
                            &&
        (string.IsNullOrEmpty(specs.SearchValue) || product.Name.ToLower().Contains(specs.SearchValue))

                 
                  )
        {
        }
    }
}
