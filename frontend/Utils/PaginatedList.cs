namespace frontend.Utils;
public class PaginatedList<T> : List<T>
{
    public int PageIndex { get;}
    public int TotalPages { get; }
    
    public bool HasPreviousPage => PageIndex > 1;

    public bool HasNextPage => PageIndex < TotalPages;
}