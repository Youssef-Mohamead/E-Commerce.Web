using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using DomainLayer.Contracts;
using DomainLayer.Models;

namespace Service.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
        
        
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        protected void AddCriteria(Expression<Func<TEntity, bool>> criteriaExpression)
        {
            Criteria = criteriaExpression;
        }
        #region Include
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];


        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        => IncludeExpressions.Add(includeExpression);
        #endregion
        #region Sorting

        public Expression<Func<TEntity, object>> OrderBy { get; private set; }

        public Expression<Func<TEntity, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExp) => OrderBy = orderByExp;
        protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExp) => OrderBy = orderByDescExp;
     

        #endregion
    }
}
