using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infra.Authentication.Repository;

public static class RepositoryHelper
{
    public static IQueryable<T> IncludeMultiple<T>(this IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        where T : class
    {
        if (!includes.Any())
        {
            query = includes.Aggregate(query, 
                (current, include) => current.Include(include));
        }

        return query;
    }
}