namespace VendomaticApi.Domain.Operators.Services;

using VendomaticApi.Domain.Operators;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IOperatorRepository : IGenericRepository<Operator>
{
}

public sealed class OperatorRepository : GenericRepository<Operator>, IOperatorRepository
{
    private readonly VendomaticDbContext _dbContext;

    public OperatorRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
