namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines.Services;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteVendingMachine
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;

        public Command(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IVendingMachineRepository vendingMachineRepository, IUnitOfWork unitOfWork)
        {
            _vendingMachineRepository = vendingMachineRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _vendingMachineRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _vendingMachineRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}