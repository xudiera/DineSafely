using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data.Repositories;

public class EstablishmentRepository(ApplicationDbContext applicationDbContext) : Repository(applicationDbContext), IEstablishmentRepository
{
    public async Task<List<Establishment>> GetEstablishmentsAsync(string name, string? address)
    {
        name = name.ToLowerInvariant();
        address = address?.ToLowerInvariant();

#pragma warning disable CA1304 // Specify CultureInfo
#pragma warning disable CA1311 // Specify a culture or use an invariant version
        return await Context.Establishments
            .Where(e => EF.Functions.Like(e.Name.ToLower(), $"%{name}%") && EF.Functions.Like(e.Address.ToLower(), $"%{address}%"))
            .OrderBy(e => e.Name)
            .ThenBy(e => e.Address)
            .ThenBy(e => e.Type)
            .AsNoTracking()
            .ToListAsync();
#pragma warning restore CA1311 // Specify a culture or use an invariant version
#pragma warning restore CA1304 // Specify CultureInfo
    }
}
