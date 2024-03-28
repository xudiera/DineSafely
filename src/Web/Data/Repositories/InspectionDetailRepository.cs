using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data.Repositories;

public class InspectionDetailRepository(ApplicationDbContext applicationDbContext) : Repository(applicationDbContext), IInspectionDetailRepository
{
    public async Task<PaginatedList<InspectionDetail>> GetByEstablishmentIdAsync(int id, int pageIndex)
    {
        var totalItems = await Context.InspectionDetails
                .Join(Context.Inspections,
                    inspectionDetail => inspectionDetail.InspectionId,
                    inspection => inspection.Id,
                    (inspectionDetail, inspection) => new { InspectionDetail = inspectionDetail, Inspection = inspection }
                    )
                .OrderByDescending(e => e.Inspection.Date)
                .Where(e => e.Inspection.EstablishmentId == id)
                .CountAsync();

        var inspectionDetails = await Context.InspectionDetails
                .Join(Context.Inspections,
                    inspectionDetail => inspectionDetail.InspectionId,
                    inspection => inspection.Id,
                    (inspectionDetail, inspection) => new { InspectionDetail = inspectionDetail, Inspection = inspection }
                    )
                .OrderByDescending(e => e.Inspection.Date)
                .Where(e => e.Inspection.EstablishmentId == id)
                .Select(e => e.InspectionDetail)
                .Include(e => e.Inspection)
                .ThenInclude(e => e.Establishment)
                .Skip((pageIndex - 1) * PageSize)
                .Take(PageSize)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

        return new PaginatedList<InspectionDetail>(inspectionDetails, totalItems, pageIndex, PageSize);
    }
}
