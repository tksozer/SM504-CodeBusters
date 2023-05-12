namespace VendomaticApi.Domain.RolePermissions.Services;

using VendomaticApi.Domain.RolePermissions;
using VendomaticApi.Databases;
using VendomaticApi.Services;

public interface IRolePermissionRepository : IGenericRepository<RolePermission>
{
}

public sealed class RolePermissionRepository : GenericRepository<RolePermission>, IRolePermissionRepository
{
    private readonly VendomaticDbContext _dbContext;

    public RolePermissionRepository(VendomaticDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}
