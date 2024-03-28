namespace Web.Data;

/// <summary>
/// Represents a paginated list of items.
/// </summary>
/// <typeparam name="T">The type of items in the list.</typeparam>
public class PaginatedList<T> : List<T>, IPaginatedList
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class.
    /// </summary>
    /// <param name="items">The items on the page.</param>
    /// <param name="totalItems">The total count of items that match the query.</param>
    /// <param name="pageIndex">The page index to get.</param>
    /// <param name="pageSize">The page size to create.</param>
    public PaginatedList(IEnumerable<T> items, int totalItems, int pageIndex, int pageSize)
    {
        if (pageIndex < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageIndex), "Page index must be greater than or equal to 1.");
        }

        if (pageSize <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");
        }

        AddRange(items);
        PageIndex = pageIndex;

        // Ensures that the total pages calculation is rounded up, providing an accurate count for partial pages.
        TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
    }

    /// <summary>
    /// Gets a value indicating whether it exists a next page.
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Gets a value indicating whether it exists a previous page.
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// Gets the current page index.
    /// </summary>
    public int PageIndex { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages { get; }
}
