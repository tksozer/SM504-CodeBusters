namespace VendomaticApi.Domain.MachineOperators.Features;

using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Domain.MachineOperators.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.MachineOperators.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateMachineOperator
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly MachineOperatorForUpdateDto UpdatedMachineOperatorData;

        public Command(Guid id, MachineOperatorForUpdateDto updatedMachineOperatorData)
        {
            Id = id;
            UpdatedMachineOperatorData = updatedMachineOperatorData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IMachineOperatorRepository _machineOperatorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IMachineOperatorRepository machineOperatorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _machineOperatorRepository = machineOperatorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var machineOperatorToUpdate = await _machineOperatorRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var machineOperatorToAdd = _mapper.Map<MachineOperatorForUpdate>(request.UpdatedMachineOperatorData);
            machineOperatorToUpdate.Update(machineOperatorToAdd);

            _machineOperatorRepository.Update(machineOperatorToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}