namespace VendomaticApi.Domain.Operators.Features;

using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Domain.Operators.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetOperator
{
    public sealed class Query : IRequest<OperatorDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, OperatorDto>
    {
        private readonly IOperatorRepository _operatorRepository;
        private readonly IMapper _mapper;

        public Handler(IOperatorRepository operatorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _operatorRepository = operatorRepository;
        }

        public async Task<OperatorDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _operatorRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<OperatorDto>(result);
        }
    }
}