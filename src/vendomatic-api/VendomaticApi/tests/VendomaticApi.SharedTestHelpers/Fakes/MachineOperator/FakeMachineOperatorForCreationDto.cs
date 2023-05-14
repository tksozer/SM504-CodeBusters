namespace VendomaticApi.SharedTestHelpers.Fakes.MachineOperator;

using AutoBogus;
using VendomaticApi.Domain.MachineOperators;
using VendomaticApi.Domain.MachineOperators.Dtos;

public sealed class FakeMachineOperatorForCreationDto : AutoFaker<MachineOperatorForCreationDto>
{
    public FakeMachineOperatorForCreationDto()
    {
    }
}