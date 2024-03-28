using Web.Data;

namespace Web.ViewModels;

/// <summary>
/// Provides a class for the pagination view model.
/// </summary>
public class PaginationViewModel
{
    /// <summary>
    /// Gets or sets the href of the pagination.
    /// </summary>
    public required string Href { get; set; }

    /// <summary>
    /// Gets or sets the paginated list.
    /// </summary>
    public required IPaginatedList PaginatedList { get; set; }
}
