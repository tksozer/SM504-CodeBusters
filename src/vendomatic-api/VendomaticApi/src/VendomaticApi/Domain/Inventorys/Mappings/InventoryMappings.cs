namespace VendomaticApi.Domain.Inventorys.Mappings;

using VendomaticApi.Domain.Inventorys.Dtos;
using VendomaticApi.Domain.Inventorys;
using VendomaticApi.Domain.Inventorys.Models;
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