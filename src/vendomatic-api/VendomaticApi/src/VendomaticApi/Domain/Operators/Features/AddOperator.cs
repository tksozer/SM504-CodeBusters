namespace VendomaticApi.Domain.Operators.Features;

using VendomaticApi.Domain.Operators.Services;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Domain.Operators.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddOperator
{
    public sealed class Command : IRequest<OperatorDto>
    {
        public readonly OperatorForCreationDto OperatorToAdd;

        public Command(OperatorForCreationDto operatorToAdd)
        {
            OperatorToAdd = operatorToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, OperatorDto>
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IOperatorRepository operatorRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _operatorRepository = operatorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<OperatorDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var operatorToAdd = _mapper.Map<OperatorForCreation>(request.OperatorToAdd);
            var operator = Operator.Create(operatorToAdd);

            await _operatorRepository.Add(operator, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<OperatorDto>(operator);
        }
    }
}