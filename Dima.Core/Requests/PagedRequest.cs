namespace Dima.Core.Requests;

public abstract class PagedRequest : Request
{
    public int PageNumber { get; set; } = Configuration.DEFAULT_PAGE_NUMBER;
    public int PageSize { get; set; } = Configuration.DEFAULT_PAGE_SIZE;
}
