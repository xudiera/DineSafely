
namespace Web.Data.Repositories;

public abstract class Repository(ApplicationDbContext applicationDbContext) : IDisposable
{
    public ApplicationDbContext Context { get; } = applicationDbContext;

    private bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            Context.Dispose();
        }

        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
