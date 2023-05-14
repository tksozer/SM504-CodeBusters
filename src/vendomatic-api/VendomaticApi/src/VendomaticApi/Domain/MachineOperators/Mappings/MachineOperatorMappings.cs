namespace VendomaticApi.Domain.MachineOperators.Mappings;

using VendomaticApi.Domain.MachineOperators.Dtos;
using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.Models;
using Mapster;

public sealed class MachineOperatorMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<MachineOperator, MachineOperatorDto>();
        config.NewConfig<MachineOperatorForCreationDto, MachineOperator>()
            .TwoWays();
        config.NewConfig<MachineOperatorForUpdateDto, MachineOperator>()
            .TwoWays();
        config.NewConfig<MachineOperatorForCreation, MachineOperator>()
            .TwoWays();
        config.NewConfig<MachineOperatorForUpdate, MachineOperator>()
            .TwoWays();
    }
}