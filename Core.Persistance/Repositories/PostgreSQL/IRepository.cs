using System.Linq.Expressions;
using Core.Application.Interfaces.Infrastructure.PostgreSQL;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;

namespace Core.Persistance.Repositories.PostgreSQL;

public interface IRepository<T> : IQuery<T> where T : BaseEntity
{
    T Get(Expression<Func<T, bool>> predicate);

    IPaginate<T> GetList(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true);

    IPaginate<T> GetListByDynamic(Dynamic.Dynamic dynamic,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        int index = 0,
        int size = 10,
        bool enableTracking = true);

    T Add(T entity);
    T Update(T entity);
    T Delete(T entity);
}