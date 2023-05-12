namespace VendomaticApi.Domain.Inventorys.Features;

using VendomaticApi.Domain.Inventorys.Services;
using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Dtos;
using VendomaticApi.Domain.Inventorys.Models;
using VendomaticApi.Services;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class AddInventory
{
    public sealed class Command : IRequest<InventoryDto>
    {
        public readonly InventoryForCreationDto InventoryToAdd;

        public Command(InventoryForCreationDto inventoryToAdd)
        {
            InventoryToAdd = inventoryToAdd;
        }
    }

    public sealed class Handler : IRequestHandler<Command, InventoryDto>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InventoryDto> Handle(Command request, CancellationToken cancellationToken)
        {
            var inventoryToAdd = _mapper.Map<InventoryForCreation>(request.InventoryToAdd);
            var inventory = Inventory.Create(inventoryToAdd);

            await _inventoryRepository.Add(inventory, cancellationToken);
            await _unitOfWork.CommitChanges(cancellationToken);

            return _mapper.Map<InventoryDto>(inventory);
        }
    }
}