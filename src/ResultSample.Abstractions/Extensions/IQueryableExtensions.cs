using System.Linq.Expressions;

namespace ResultSample.Abstractions.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<T> When<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> filter)
    {
        if (!condition)
            return query;

        return query.Where(filter);
    }
}