namespace VendomaticApi.Domain.MachineOperators.Services;

using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IMachineOperatorRepository : IGenericRepository<MachineOperator>
{
}

public sealed class MachineOperatorRepository : GenericRepository<MachineOperator>, IMachineOperatorRepository
{
    private readonly VendomaticDbContext _dbContext;

    public MachineOperatorRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
