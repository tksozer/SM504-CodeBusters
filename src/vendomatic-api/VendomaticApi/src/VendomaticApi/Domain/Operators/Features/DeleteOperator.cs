namespace VendomaticApi.Domain.Operators.Features;

using VendomaticApi.Domain.Operators.Services;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MediatR;

public static class DeleteOperator
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
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IOperatorRepository operatorRepository, IUnitOfWork unitOfWork)
        {
            _operatorRepository = operatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var recordToDelete = await _operatorRepository.GetById(request.Id, cancellationToken: cancellationToken);
            _operatorRepository.Remove(recordToDelete);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}