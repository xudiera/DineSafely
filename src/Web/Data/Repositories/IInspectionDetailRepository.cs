using Web.Data.DTOs;
using Web.Models;

namespace Web.Data.Repositories;

public interface IInspectionDetailRepository
{
    Task<PaginatedList<InspectionDetail>> GetByEstablishmentIdAsync(int id, int pageIndex);

    Task<List<SeverityGroupDTO>> GetGroupBySeverity(int establishmentId);
}
