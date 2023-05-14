namespace VendomaticApi.Domain.MachineOperators.Features;

using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Domain.MachineOperators.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetMachineOperator
{
    public sealed class Query : IRequest<MachineOperatorDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, MachineOperatorDto>
    {
        private readonly IMachineOperatorRepository _machineOperatorRepository;
        private readonly IMapper _mapper;

        public Handler(IMachineOperatorRepository machineOperatorRepository, IMapper mapper)
        {
            _mapper = mapper;
            _machineOperatorRepository = machineOperatorRepository;
        }

        public async Task<MachineOperatorDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _machineOperatorRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<MachineOperatorDto>(result);
        }
    }
}