namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines.Services;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddVendingMachine
{
    public sealed class Command : IRequest<VendingMachineDto>
    {
        public readonly VendingMachineForCreationDto VendingMachineToAdd;

        public Command(VendingMachineForCreationDto vendingMachineToAdd)
        {
            VendingMachineToAdd = vendingMachineToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, VendingMachineDto>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IVendingMachineRepository vendingMachineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _vendingMachineRepository = vendingMachineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<VendingMachineDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var vendingMachineToAdd = _mapper.Map<VendingMachineForCreation>(request.VendingMachineToAdd);
            var vendingMachine = VendingMachine.Create(vendingMachineToAdd);

            await _vendingMachineRepository.Add(vendingMachine, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<VendingMachineDto>(vendingMachine);
        }
    }
}