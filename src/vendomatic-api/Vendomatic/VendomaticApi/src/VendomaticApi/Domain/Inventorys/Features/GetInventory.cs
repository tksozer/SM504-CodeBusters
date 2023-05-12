namespace VendomaticApi.Domain.Inventorys.Features;

using VendomaticApi.Domain.Inventorys.Dtos;
using VendomaticApi.Domain.Inventorys.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class GetInventory
{
    public sealed class Query : IRequest<InventoryDto>
    {
        public readonly Guid Id;

        public Query(Guid id)
        {
            Id = id;
        }
    }

    public sealed class Handler : IRequestHandler<Query, InventoryDto>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IMapper _mapper;

        public Handler(IInventoryRepository inventoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<InventoryDto> Handle(Query request, CancellationToken cancellationToken)
        {
            var result = await _inventoryRepository.GetById(request.Id, cancellationToken: cancellationToken);
            return _mapper.Map<InventoryDto>(result);
        }
    }
}