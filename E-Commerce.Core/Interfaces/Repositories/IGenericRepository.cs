﻿using E_Commerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Interfaces.Repositories
{
	public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<TEntity> GetAsync(TKey id); 
		Task AddAsync(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		
		

		Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecification<TEntity> specification);
        Task<int> GetProductCountSpecAsync(ISpecification<TEntity> specification);

        Task<TEntity> GetWithSpecAsync(ISpecification<TEntity> specification);
	}
}
