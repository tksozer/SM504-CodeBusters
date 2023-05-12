namespace VendomaticApi.Domain.RolePermissions.Features;

using VendomaticApi.Domain.RolePermissions.Services;
using VendomaticApi.Domain.RolePermissions;
using VendomaticApi.Domain.RolePermissions.Dtos;
using VendomaticApi.Domain.RolePermissions.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using VendomaticApi.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class AddRolePermission
{
    public sealed class Command : IRequest<RolePermissionDto>
    {
        public readonly RolePermissionForCreationDto RolePermissionToAdd;

        public Command(RolePermissionForCreationDto rolePermissionToAdd)
        {
            RolePermissionToAdd = rolePermissionToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, RolePermissionDto>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork, IMapper mapper, IHeimGuardClient heimGuard)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
            _heimGuard = heimGuard;
        }

        public async Task<RolePermissionDto> Handle(Command request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanAddRolePermissions);

            var rolePermissionToAdd = _mapper.Map<RolePermissionForCreation>(request.RolePermissionToAdd);
            var rolePermission = RolePermission.Create(rolePermissionToAdd);

            await _rolePermissionRepository.Add(rolePermission, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<RolePermissionDto>(rolePermission);
        }
    }
}