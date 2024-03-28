
using Web.Models;

namespace Web.Data.Repositories;

public interface IEstablishmentRepository
{
    Task<PaginatedList<Establishment>> GetEstablishmentsAsync(string name, string? address, int pageIndex);
}
