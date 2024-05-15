using E_Commerce.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Repository.Specifications
{
	public class BaseSpecifications<T> : ISpecification<T>
	{
		public BaseSpecifications(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}

		public Expression<Func<T, bool>> Criteria { get; }
		public List<Expression<Func<T, object>>> IncludeExpressions { get; } = new();

        public Expression<Func<T, object>> OrderBy { get; protected set; }

        public Expression<Func<T, object>> OrderByDesc { get; protected set; }

        public int Skip { get; protected set; }

        public int Take { get; protected set; }

        public bool IsPagenated { get; protected set; }
		protected void ApplyPagenated(int PageSize,int PageIndex) { 
		
		IsPagenated=true;
			Take = PageSize;
			Skip=(PageIndex -1)* PageSize;
		}
    }
}
