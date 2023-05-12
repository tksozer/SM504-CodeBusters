namespace VendomaticApi.SharedTestHelpers.Fakes.RolePermission;

using AutoBogus;
using VendomaticApi.Domain;
using VendomaticApi.Domain.RolePermissions.Dtos;
using VendomaticApi.Domain.Roles;
using VendomaticApi.Domain.RolePermissions.Models;

public sealed class FakeRolePermissionForUpdateDto : AutoFaker<RolePermissionForUpdateDto>
{
    public FakeRolePermissionForUpdateDto()
    {
        RuleFor(rp => rp.Permission, f => f.PickRandom(Permissions.List()));
        RuleFor(rp => rp.Role, f => f.PickRandom(Role.ListNames()));
    }
}