namespace VendomaticApi.Domain.RolePermissions.Features;

using VendomaticApi.Domain.RolePermissions;
using VendomaticApi.Domain.RolePermissions.Dtos;
using VendomaticApi.Domain.RolePermissions.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.RolePermissions.Models;
using SharedKernel.Exceptions;
using VendomaticApi.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class UpdateRolePermission
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly RolePermissionForUpdateDto UpdatedRolePermissionData;

        public Command(Guid id, RolePermissionForUpdateDto updatedRolePermissionData)
        {
            Id = id;
            UpdatedRolePermissionData = updatedRolePermissionData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork, IMapper mapper, IHeimGuardClient heimGuard)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _heimGuard = heimGuard;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanUpdateRolePermissions);

            var rolePermissionToUpdate = await _rolePermissionRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var rolePermissionToAdd = _mapper.Map<RolePermissionForUpdate>(request.UpdatedRolePermissionData);
            rolePermissionToUpdate.Update(rolePermissionToAdd);

            _rolePermissionRepository.Update(rolePermissionToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}