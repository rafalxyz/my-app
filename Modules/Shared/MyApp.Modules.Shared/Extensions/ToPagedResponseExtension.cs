using MyApp.Modules.Shared.Types;

namespace MyApp.Modules.Shared.Extensions;

public static class ToPagedResponseExtension
{
    public static PagedResponse<TItem> ToPagedResponse<TItem>(this IQueryable<TItem> query, BaseSearchDTO searchDto)
    {
        var totalCount = query.Count();

        var items = query.Skip((searchDto.PageNumber - 1) * searchDto.PageSize).Take(searchDto.PageSize).ToList();

        return new PagedResponse<TItem>
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}
