namespace VendomaticApi.Domain.RolePermissions.Mappings;

using VendomaticApi.Domain.RolePermissions.Dtos;
using VendomaticApi.Domain.RolePermissions;
using VendomaticApi.Domain.RolePermissions.Models;
using Mapster;

public sealed class RolePermissionMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RolePermission, RolePermissionDto>();
        config.NewConfig<RolePermissionForCreationDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForUpdateDto, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForCreation, RolePermission>()
            .TwoWays();
        config.NewConfig<RolePermissionForUpdate, RolePermission>()
            .TwoWays();
    }
}