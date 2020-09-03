using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QHomeGroup.Data.EF.Connector;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.Interfaces;
using QHomeGroup.Infrastructure.SharedKernel;
using QHomeGroup.Utilities.Dtos;
using QHomeGroup.Utilities.Extensions;

namespace QHomeGroup.Data.EF.Abstract
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDbContext _appContext;

        private bool disposed;

        public EFRepository(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<QueryResult<T>> QueryAsync(Expression<Func<T, bool>> predicate, int skip, int take)
        {
            var queryable = _appContext.Set<T>().Where(predicate);
            return await queryable.ToQueryResultAsync(skip, take);
        }

        public async Task<IList<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appContext.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _appContext.Set<T>().ToListAsync();
        }

        public async Task<T> FindByIdAsync(K id)
        {
            return await _appContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appContext.Set<T>().AnyAsync(predicate);
        }

        public async Task<T> FindSingleAsync(Expression<Func<T, bool>> predicate)
        {
            return await _appContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _appContext.Add(entity);
        }

        public void Update(T entity)
        {
            _appContext.Entry(entity).State = EntityState.Modified;
        }

        public void Remove(T entity)
        {
            _appContext.Set<T>().Remove(entity);
        }

        public void RemoveMultiple(IList<T> entities)
        {
            _appContext.Set<T>().RemoveRange(entities);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing) _appContext.Dispose();

                disposed = true;
            }
        }
    }
}