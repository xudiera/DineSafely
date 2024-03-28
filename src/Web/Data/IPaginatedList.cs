namespace Web.Data;

/// <summary>
/// Used to get paginated data from the database.
/// </summary>
public interface IPaginatedList
{
    /// <summary>
    /// Gets a value indicating whether it exists a next page.
    /// </summary>
    bool HasNextPage { get; }

    /// <summary>
    /// Gets a value indicating whether it exists a previous page.
    /// </summary>
    bool HasPreviousPage { get; }

    /// <summary>
    /// Gets the current page index.
    /// </summary>
    int PageIndex { get; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    int TotalPages { get; }
}
