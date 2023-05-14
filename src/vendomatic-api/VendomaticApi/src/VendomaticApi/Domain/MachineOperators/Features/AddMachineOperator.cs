namespace VendomaticApi.Domain.MachineOperators.Features;

using VendomaticApi.Domain.MachineOperators.Services;
using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Domain.MachineOperators.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddMachineOperator
{
    public sealed class Command : IRequest<MachineOperatorDto>
    {
        public readonly MachineOperatorForCreationDto MachineOperatorToAdd;

        public Command(MachineOperatorForCreationDto machineOperatorToAdd)
        {
            MachineOperatorToAdd = machineOperatorToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, MachineOperatorDto>
    {
        private readonly IMachineOperatorRepository _machineOperatorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IMachineOperatorRepository machineOperatorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _machineOperatorRepository = machineOperatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MachineOperatorDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var machineOperatorToAdd = _mapper.Map<MachineOperatorForCreation>(request.MachineOperatorToAdd);
            var machineOperator = MachineOperator.Create(machineOperatorToAdd);

            await _machineOperatorRepository.Add(machineOperator, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<MachineOperatorDto>(machineOperator);
        }
    }
}