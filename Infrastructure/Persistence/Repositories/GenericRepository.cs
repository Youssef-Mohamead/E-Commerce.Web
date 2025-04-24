using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool TrackChanges = false) => await _dbContext.Set<TEntity>().ToListAsync();
        public async Task<TEntity?> GetByIdAsunc(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);
        public async Task AddAsync(TEntity entity) =>  await _dbContext.Set<TEntity>().AddAsync(entity);
        
        public  void Remove(TEntity entity) =>  _dbContext.Set<TEntity>().Remove(entity);

        public void Update(TEntity entity) =>  _dbContext.Set<TEntity>().Update(entity);

        #region With Specifications
        public async Task<TEntity?> GetByIdAsunc(ISpecifications<TEntity, TKey> specifications)
        {
         return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();


        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();

        }
        #endregion
    }
}
