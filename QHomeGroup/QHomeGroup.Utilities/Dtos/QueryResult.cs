using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace QHomeGroup.Utilities.Dtos
{
    public class QueryResult<T>
    {
        public long Count { get; set; }
        public IEnumerable<T> Items { get; set; }

        public QueryResult()
        {
        }

        public QueryResult(long count, IEnumerable<T> items)
        {
            Count = count;
            Items = items;
        }
    }

    public static class QueryResultExtension
    {
        public static async Task<QueryResult<T>> ToQueryResultAsync<T>(this IQueryable<T> queryable, int skip, int take)
        {
            return new QueryResult<T>
            {
                Count = await queryable.CountAsync(),
                Items = await queryable.Skip(skip).Take(take).ToListAsync()
            };
        }
    }
}
