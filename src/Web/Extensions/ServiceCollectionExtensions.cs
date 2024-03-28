using Web.Data.Repositories;

namespace Web.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IEstablishmentRepository, EstablishmentRepository>();
        services.AddScoped<IInspectionDetailRepository, InspectionDetailRepository>();
    }
}
