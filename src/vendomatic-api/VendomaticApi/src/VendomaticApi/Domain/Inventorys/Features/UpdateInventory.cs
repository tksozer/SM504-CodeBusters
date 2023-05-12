namespace VendomaticApi.Domain.Inventorys.Features;

using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Dtos;
using VendomaticApi.Domain.Inventorys.Services;
using VendomaticApi.Services;
using VendomaticApi.Domain.Inventorys.Models;
using SharedKernel.Exceptions;
using MapsterMapper;
using MediatR;

public static class UpdateInventory
{
    public sealed class Command : IRequest<bool>
    {
        public readonly Guid Id;
        public readonly InventoryForUpdateDto UpdatedInventoryData;

        public Command(Guid id, InventoryForUpdateDto updatedInventoryData)
        {
            Id = id;
            UpdatedInventoryData = updatedInventoryData;
        }
    }

    public sealed class Handler : IRequestHandler<Command, bool>
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public Handler(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
        {
            var inventoryToUpdate = await _inventoryRepository.GetById(request.Id, cancellationToken: cancellationToken);

            var inventoryToAdd = _mapper.Map<InventoryForUpdate>(request.UpdatedInventoryData);
            inventoryToUpdate.Update(inventoryToAdd);

            _inventoryRepository.Update(inventoryToUpdate);
            return await _unitOfWork.CommitChanges(cancellationToken) >= 1;
        }
    }
}