namespace MyApp.Modules.Shared.Types;

public enum OrderDirection
{
    Asc = 0,
    Desc = 1,
}

public class BaseSearchDTO
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public string? OrderBy { get; set; }

    public OrderDirection OrderDirection { get; set; } = OrderDirection.Asc;
}
