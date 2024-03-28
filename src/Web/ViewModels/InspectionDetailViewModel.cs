using Web.Data;
using Web.Models;

namespace Web.ViewModels;

public class InspectionDetailViewModel
{
    public required int Id { get; set; }
    public required PaginatedList<InspectionDetail> InspectionDetails { get; set; }
}
