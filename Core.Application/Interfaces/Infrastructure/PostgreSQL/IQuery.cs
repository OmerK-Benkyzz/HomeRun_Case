namespace Core.Application.Interfaces.Infrastructure.PostgreSQL;

public interface IQuery<T>
{
    IQueryable<T> Query();
}