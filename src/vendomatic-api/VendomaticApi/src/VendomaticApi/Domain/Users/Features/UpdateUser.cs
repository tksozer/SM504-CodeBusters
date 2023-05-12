namespace VendomaticApi.Domain.Users.Features;

using VendomaticApi.Domain.Users;
using VendomaticApi.Domain.Users.Dtos;
using VendomaticApi.Domain.Users.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.Users.Models;
using SharedKernel.Exceptions;
using VendomaticApi.Domain;
using HeimGuard;
using MapsterMapper;
using MediatR;

public static class UpdateUser
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly UserForUpdateDto UpdatedUserData;

        public Command(Guid id, UserForUpdateDto updatedUserData)
        {
            Id = id;
            UpdatedUserData = updatedUserData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHeimGuardClient _heimGuard;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IHeimGuardClient heimGuard)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _heimGuard = heimGuard;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            await _heimGuard.MustHavePermission<ForbiddenAccessException>(Permissions.CanUpdateUsers);

            var userToUpdate = await _userRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var userToAdd = _mapper.Map<UserForUpdate>(request.UpdatedUserData);
            userToUpdate.Update(userToAdd);

            _userRepository.Update(userToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}