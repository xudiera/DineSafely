using Web.Models;

namespace Web.Data.Repositories;

public interface IInspectionDetailRepository
{
    Task<PaginatedList<InspectionDetail>> GetByEstablishmentIdAsync(int id, int pageIndex);
}
