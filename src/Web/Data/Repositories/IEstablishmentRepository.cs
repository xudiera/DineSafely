
using Web.Models;

namespace Web.Data.Repositories;

public interface IEstablishmentRepository
{
    Task<List<Establishment>> GetEstablishmentsAsync(string name, string? address);
}
