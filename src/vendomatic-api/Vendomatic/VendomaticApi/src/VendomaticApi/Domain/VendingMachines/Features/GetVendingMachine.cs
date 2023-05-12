namespace VendomaticApi.Domain.VendingMachines.Features;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetVendingMachine
{
    public sealed class Query : IRequest<VendingMachineDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, VendingMachineDto>
    {
        private readonly IVendingMachineRepository _vendingMachineRepository;
        private readonly IMapper _mapper;

        public Handler(IVendingMachineRepository vendingMachineRepository, IMapper mapper)
        {
            _mapper = mapper;
            _vendingMachineRepository = vendingMachineRepository;
        }

        public async Task<VendingMachineDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _vendingMachineRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<VendingMachineDto>(result);
        }
    }
}