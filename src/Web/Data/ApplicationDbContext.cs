using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Establishment> Establishments { get; set; } = null!;
    public DbSet<Inspection> Inspections { get; set; } = null!;
    public DbSet<InspectionDetail> InspectionDetails { get; set; } = null!;
}
