namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.VendingMachines.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateVendingMachine
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly VendingMachineForUpdateDto UpdatedVendingMachineData;

        public Command(Guid id, VendingMachineForUpdateDto updatedVendingMachineData)
        {
            Id = id;
            UpdatedVendingMachineData = updatedVendingMachineData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IVendingMachineRepository vendingMachineRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _vendingMachineRepository = vendingMachineRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var vendingMachineToUpdate = await _vendingMachineRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var vendingMachineToAdd = _mapper.Map<VendingMachineForUpdate>(request.UpdatedVendingMachineData);
            vendingMachineToUpdate.Update(vendingMachineToAdd);

            _vendingMachineRepository.Update(vendingMachineToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}
