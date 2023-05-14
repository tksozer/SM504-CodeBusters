namespace VendomaticApi.Domain.Operators.Features;

using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Domain.Operators.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.Operators.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateOperator
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly OperatorForUpdateDto UpdatedOperatorData;

        public Command(Guid id, OperatorForUpdateDto updatedOperatorData)
        {
            Id = id;
            UpdatedOperatorData = updatedOperatorData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IOperatorRepository operatorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _operatorRepository = operatorRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var operatorToUpdate = await _operatorRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var operatorToAdd = _mapper.Map<OperatorForUpdate>(request.UpdatedOperatorData);
            operatorToUpdate.Update(operatorToAdd);

            _operatorRepository.Update(operatorToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}