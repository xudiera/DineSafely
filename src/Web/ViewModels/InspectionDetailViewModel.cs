using Web.Data;
using Web.Models;

namespace Web.ViewModels;

public class InspectionDetailViewModel
{
    public required int Id { get; set; }

    public string MapUrl
    {
        get
        {
            var establishment = InspectionDetails.First().Inspection.Establishment;
            return $"https://www.google.com/maps/search/?api=1&query={establishment.Name}%2C{establishment.Address}%2CToronto%2CON";
        }
    }

    public required PaginatedList<InspectionDetail> InspectionDetails { get; set; }
}
