namespace VendomaticApi.Domain.Inventorys.Services;

using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IInventoryRepository : IGenericRepository<Inventory>
{
}

public sealed class InventoryRepository : GenericRepository<Inventory>, IInventoryRepository
{
    private readonly VendomaticDbContext _dbContext;

    public InventoryRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
