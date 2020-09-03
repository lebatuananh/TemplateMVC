using QHomeGroup.Utilities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace QHomeGroup.Infrastructure.Interfaces
{
    public interface IRepository<T, K> where T : class
    {
        Task<QueryResult<T>> QueryAsync(Expression<Func<T, bool>> predicate, int skip, int take);

        Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predicate);

        Task<IList<T>> GetAllAsync();
        Task<T> FindByIdAsync(K id);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        void RemoveMultiple(IList<T> entities);
    }
}