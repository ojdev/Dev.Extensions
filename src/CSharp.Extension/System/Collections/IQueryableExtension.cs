using System.Linq;
using System.Linq.Expressions;

namespace System.Collections.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public static class IQueryableExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
