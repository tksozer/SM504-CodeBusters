namespace VendomaticApi.Domain.VendingMachines.Services;

using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IVendingMachineRepository : IGenericRepository<VendingMachine>
{
}

public sealed class VendingMachineRepository : GenericRepository<VendingMachine>, IVendingMachineRepository
{
    private readonly VendomaticDbContext _dbContext;

    public VendingMachineRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
