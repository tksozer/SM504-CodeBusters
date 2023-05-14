namespace VendomaticApi.Domain.Inventories.Mappings;

using VendomaticApi.Domain.Inventories.Dtos;
using VendomaticApi.Domain.Inventories;
using VendomaticApi.Domain.Inventories.Models;
using Mapster;

public sealed class InventoryMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Inventory, InventoryDto>();
        config.NewConfig<InventoryForCreationDto, Inventory>()
            .TwoWays();
        config.NewConfig<InventoryForUpdateDto, Inventory>()
            .TwoWays();
        config.NewConfig<InventoryForCreation, Inventory>()
            .TwoWays();
        config.NewConfig<InventoryForUpdate, Inventory>()
            .TwoWays();
    }
}