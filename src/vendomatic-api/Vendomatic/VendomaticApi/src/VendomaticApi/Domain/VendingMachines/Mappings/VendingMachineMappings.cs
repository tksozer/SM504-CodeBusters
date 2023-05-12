namespace VendomaticApi.Domain.VendingMachines.Mappings;

using VendomaticApi.Domain.VendingMachines.Dtos;
using VendomaticApi.Domain.VendingMachines;
using VendomaticApi.Domain.VendingMachines.Models;
using Mapster;

public sealed class VendingMachineMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VendingMachine, VendingMachineDto>();
        config.NewConfig<VendingMachineForCreationDto, VendingMachine>()
            .TwoWays();
        config.NewConfig<VendingMachineForUpdateDto, VendingMachine>()
            .TwoWays();
        config.NewConfig<VendingMachineForCreation, VendingMachine>()
            .TwoWays();
        config.NewConfig<VendingMachineForUpdate, VendingMachine>()
            .TwoWays();
    }
}