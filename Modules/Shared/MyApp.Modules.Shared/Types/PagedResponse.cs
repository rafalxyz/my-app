namespace MyApp.Modules.Shared.Types;

public class PagedResponse<TItem>
{
    public int TotalCount { get; set; }

    public IEnumerable<TItem> Items { get; set; } = new List<TItem>();
}
