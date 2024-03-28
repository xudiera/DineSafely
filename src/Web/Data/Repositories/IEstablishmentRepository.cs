
using Web.Models;

namespace Web.Data.Repositories;

public interface IEstablishmentRepository
{
    Task<PaginatedList<Establishment>> GetAsync(string name, string? address, int pageIndex);

    Task<Establishment?> GetByIdAsync(int id);
}
