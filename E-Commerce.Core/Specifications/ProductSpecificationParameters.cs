using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Specifications
{
    public class ProductSpecificationParameters
    {
        private const int MaxPageSize = 10;
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public ProductSortingParameters? Sort { get; set; }
        public int PageIndex { get; set; } = 1;
        private int _PageSize = 5;

        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize= value  > MaxPageSize ? MaxPageSize : value; }
        }

        private string? _SearchValue;

        public string? SearchValue
        {
            get { return _SearchValue; }
            set { _SearchValue = value?.Trim().ToLower(); }
        }

    }
}
