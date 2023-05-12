namespace VendomaticApi.Services;

using VendomaticApi.Databases;

public interface IUnitOfWork : IVendomaticApiScopedService
{
    Task<int> CommitChanges(CancellationToken cancellationToken = default);
}

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly VendomaticDbContext _dbContext;

    public UnitOfWork(VendomaticDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CommitChanges(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
