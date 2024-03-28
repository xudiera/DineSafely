using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data.Repositories;

public class EstablishmentRepository(ApplicationDbContext applicationDbContext) : Repository(applicationDbContext), IEstablishmentRepository
{
    public async Task<PaginatedList<Establishment>> GetAsync(string name, string? address, int pageIndex)
    {
        name = name.ToLowerInvariant();
        address = address?.ToLowerInvariant();

#pragma warning disable CA1304 // Specify CultureInfo
#pragma warning disable CA1311 // Specify a culture or use an invariant version
        var totalItems = await Context.Establishments
            .CountAsync(e => EF.Functions.Like(e.Name.ToLower(), $"%{name}%") && EF.Functions.Like(e.Address.ToLower(), $"%{address}%"));

        var establishments = await Context.Establishments
            .Where(e => EF.Functions.Like(e.Name.ToLower(), $"%{name}%") && EF.Functions.Like(e.Address.ToLower(), $"%{address}%"))
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Address)
            .ThenBy(e => e.Type)
            .Skip((pageIndex - 1) * PageSize)
            .Take(PageSize)
            .AsNoTracking()
            .ToListAsync();
#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1304 // Specify CultureInfo

        return new PaginatedList<Establishment>(establishments, totalItems, pageIndex, PageSize);
    }

    public async Task<Establishment?> GetByIdAsync(int id)
        => await Context
            .Establishments
            .Include(establishment => establishment
                .Inspections
                .OrderByDescending(inspection => inspection.Date))
            .ThenInclude(inspection => inspection.InspectionDetails)
            .AsSplitQuery()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
}
