using AutoMapper;
using Core.Application.Interfaces.Services;
using Core.Application.Services;

namespace Core.Application.Helper;

public class PaginateConverter<TSource, TDestination> : ITypeConverter<IPaginate<TSource>, IPaginate<TDestination>>
{
    public IPaginate<TDestination> Convert(IPaginate<TSource> source,
        IPaginate<TDestination> destination,
        ResolutionContext context)
    {
        return Paginate.From(source, items => context.Mapper.Map<IEnumerable<TDestination>>(items));
    }
}