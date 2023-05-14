namespace VendomaticApi.Domain.MachineOperators.Features;

using VendomaticApi.Domain.MachineOperators.Services;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteMachineOperator
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
        private readonly IMachineOperatorRepository _machineOperatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IMachineOperatorRepository machineOperatorRepository, IUnitOfWork unitOfWork)
        {
            _machineOperatorRepository = machineOperatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _machineOperatorRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _machineOperatorRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}