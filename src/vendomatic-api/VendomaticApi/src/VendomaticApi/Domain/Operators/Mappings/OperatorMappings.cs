namespace VendomaticApi.Domain.Operators.Mappings;

using VendomaticApi.Domain.Operators.Dtos;
using VendomaticApi.Domain.Operators;
using VendomaticApi.Domain.Operators.Models;
using Mapster;

public sealed class OperatorMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Operator, OperatorDto>();
        config.NewConfig<OperatorForCreationDto, Operator>()
            .TwoWays();
        config.NewConfig<OperatorForUpdateDto, Operator>()
            .TwoWays();
        config.NewConfig<OperatorForCreation, Operator>()
            .TwoWays();
        config.NewConfig<OperatorForUpdate, Operator>()
            .TwoWays();
    }
}